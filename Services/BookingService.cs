using RestaurantBookingAPI.Models.Enums;
using RestaurantBookingAPI.DTOs;
using RestaurantBookingAPI.Mappers;
using RestaurantBookingAPI.Models.Entities;
using RestaurantBookingAPI.Repositories.IRepositores;
using RestaurantBookingAPI.Services.IServices;

namespace RestaurantBookingAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ITableService _tableService;
        private readonly ICustomerRepository _customerRepository;

        public BookingService(IBookingRepository bookingRepository, ITableService tableService, ICustomerRepository customerRepository)
        {
            _bookingRepository = bookingRepository;
            _tableService = tableService;
            _customerRepository = customerRepository;
        }
        

        public async Task<IEnumerable<BookingDTO>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllBookingsAsync();
            return bookings.Select(DomainMapper.ToBookingDTO);
        }
        public async Task<BookingDTO?> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            return booking != null ? DomainMapper.ToBookingDTO(booking) : null;
        }
        public async Task<IEnumerable<BookingDTO>> GetBookingsByCustomerAsync(int customerId)
        {
            var bookings = await _bookingRepository.GetBookingsByCustomerIdAsync(customerId);
            return bookings.Select(DomainMapper.ToBookingDTO);
        }

        public async Task<IEnumerable<BookingDTO>> GetBookingsByDateAsync(DateTime date)
        {
            var bookings = await _bookingRepository.GetBookingsByDateAsync(date);
            return bookings.Select(DomainMapper.ToBookingDTO);
        }

        public async Task<BookingDTO> CreateBookingAsync(CreateBookingDTO createBookingDto)
        {
            var validation = await _tableService.ValidateBookingTimeAsync(createBookingDto.StartDateTime,createBookingDto.NumberOfGuests);
            if (!validation.IsValid)
            {
                throw new ArgumentException(validation.ErrorMessage);
            }
            var customer = await _customerRepository.GetCustomerByIdAsync(createBookingDto.CustomerId);
            if (customer == null)
            {
                throw new ArgumentException($"Customer with ID {createBookingDto.CustomerId} not found.");
            }
            var availableTableId = await _bookingRepository.FindFirstAvailableTableAsync(
                 createBookingDto.StartDateTime,
                           createBookingDto.NumberOfGuests);

            if (!availableTableId.HasValue)
            {
                throw new InvalidOperationException($"No tables available for {createBookingDto.NumberOfGuests} guests at {createBookingDto.StartDateTime:yyyy-MM-dd HH:mm}");
            }
            var booking = new Booking
            {
                CustomerId = createBookingDto.CustomerId,
                TableId = availableTableId.Value,
                StartDateTime = createBookingDto.StartDateTime,
                NumberOfGuests = createBookingDto.NumberOfGuests,
                Status = BookingStatus.Confirmed, // Auto-confirm as designed
                CreatedAt = DateTime.UtcNow
            };
            var createdBooking = await _bookingRepository.CreateBookingAsync(booking);
            var fullBooking = await _bookingRepository.GetBookingByIdAsync(createdBooking.Id);
            return DomainMapper.ToBookingDTO(fullBooking!);
        }
        public async Task<BookingDTO> UpdateBookingAsync(int id, UpdateBookingDTO updateBookingDto)
        {
            var existingBooking = await _bookingRepository.GetBookingByIdAsync(id);
            if (existingBooking == null)
            {
                throw new ArgumentException($"Booking with ID {id} not found.");
            }
            if (!await CanModifyBookingAsync(id))
            {
                throw new InvalidOperationException("This booking cannot be modified.");
            }
            // If date/time or party size changed, check availability
            if (existingBooking.StartDateTime != updateBookingDto.StartDateTime ||
                existingBooking.NumberOfGuests != updateBookingDto.NumberOfGuests)
            {
                 if(existingBooking.NumberOfGuests!= updateBookingDto.NumberOfGuests)
                {
                    var newTableId = await _bookingRepository.FindFirstAvailableTableAsync(updateBookingDto.StartDateTime, updateBookingDto.NumberOfGuests);
                    if (!newTableId.HasValue)
                    {
                        throw new InvalidOperationException($"No tables available for {updateBookingDto.NumberOfGuests} guests at {updateBookingDto.StartDateTime:yyyy-MM-dd HH:mm}");
                    }
                    existingBooking.TableId = newTableId.Value;
                }
                else
                {
                    var isAvailable = await _bookingRepository.IsTableAvailableAsync(
                        existingBooking.TableId,
                        updateBookingDto.StartDateTime);
                    if(!isAvailable)
                    {
                        var newTableId = await _bookingRepository.FindFirstAvailableTableAsync(updateBookingDto.StartDateTime, updateBookingDto.NumberOfGuests);
                        if (!newTableId.HasValue)
                        {
                            throw new InvalidOperationException($"No tables available for {updateBookingDto.NumberOfGuests} guests at {updateBookingDto.StartDateTime:yyyy-MM-dd HH:mm}");
                        }
                        existingBooking.TableId = newTableId.Value;
                    }
                }
            }
            existingBooking.StartDateTime = updateBookingDto.StartDateTime;
            existingBooking.NumberOfGuests = updateBookingDto.NumberOfGuests;
            existingBooking.Status = updateBookingDto.Status;
            existingBooking.UpdatedAt = DateTime.UtcNow;
            var updatedBooking = await _bookingRepository.UpdateBookingAsync(existingBooking);
            var fullBooking = await _bookingRepository.GetBookingByIdAsync(updatedBooking.Id);
            return DomainMapper.ToBookingDTO(fullBooking!);
        }

        public async Task<bool> CancelBookingAsync(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking == null) return false;

            booking.Status = BookingStatus.Cancelled;
            await _bookingRepository.UpdateBookingAsync(booking);
            return true;
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            return await _bookingRepository.DeleteBookingAsync(id);
        }

        public async Task<bool> MarkBookingAsCompletedAsync(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking == null) return false;

            booking.Status = BookingStatus.Completed;
            await _bookingRepository.UpdateBookingAsync(booking);
            return true;
        }

        public async Task<bool> MarkBookingAsNoShowAsync(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking == null) return false;

            booking.Status = BookingStatus.NoShow;
            await _bookingRepository.UpdateBookingAsync(booking);
            return true;
        }
        public async Task<IEnumerable<BookingSummaryDTO>> GetTodayBookingsAsync()
        {
            var bookings = await _bookingRepository.GetBookingsByDateAsync(DateTime.Today);
            return bookings.Select(DomainMapper.ToBookingSummaryDTO);
        }

        public async Task<IEnumerable<BookingSummaryDTO>> GetActiveBookingsAsync()
        {
            var bookings = await _bookingRepository.GetActiveBookingsAsync();
            return bookings.Select(DomainMapper.ToBookingSummaryDTO);
        }

        public async Task<IEnumerable<BookingSummaryDTO>> GetUpcomingBookingsAsync(int days = 7)
        {
            var startTime = DateTime.Now;
            var endTime = DateTime.Now.AddDays(days);

            var bookings = await _bookingRepository.GetBookingsInTimeRangeAsync(startTime, endTime);
            return bookings.Where(b => b.Status == BookingStatus.Confirmed)
                          .Select(DomainMapper.ToBookingSummaryDTO);
        }

        public async Task<bool> CanModifyBookingAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (booking == null) return false;

            
            if (booking.StartDateTime <= DateTime.Now)
                return false;

            
            if (booking.Status == BookingStatus.Cancelled || booking.Status == BookingStatus.Completed)
                return false;

            return true;
        }

    }
}

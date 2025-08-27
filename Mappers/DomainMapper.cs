using RestaurantBookingAPI.DTOs;
using RestaurantBookingAPI.Models.Entities;

namespace RestaurantBookingAPI.Mappers
{
    public static class DomainMapper
    {
        #region Table Mapping
        public static TableDTO ToTableDTO(Table table, int currentBookings = 0)
        {
            return new TableDTO
            {
                Id = table.Id,
                TableNumber = table.TableNumber,
                SeatingCapacity = table.SeatingCapacity,
                IsAvailable = table.IsAvailable,
                CurrentBookings = currentBookings
            };
        }

        public static TableSummaryDTO ToTableSummaryDTO(Table table, string status)
        {
            return new TableSummaryDTO
            {
                Id = table.Id,
                TableNumber = table.TableNumber,
                SeatingCapacity = table.SeatingCapacity,
                IsAvailable = table.IsAvailable,
                Status = status
            };
        }

        public static AvailabilityResponseDTO ToAvailabilityResponseDTO(
            bool isAvailable,
            int? availableTableId,
            Table? table,
            IEnumerable<int> allAvailableTableIds,
            DateTime requestedDateTime,
            int numberOfGuests)
        {
            if (isAvailable && table != null)
            {
                return new AvailabilityResponseDTO
                {
                    IsAvailable = true,
                    AvailableTableId = availableTableId,
                    TableNumber = table.TableNumber,
                    AllAvailableTableIds = allAvailableTableIds.ToList(),
                    Message = $"Table {table.TableNumber} is available for {numberOfGuests} guests at {requestedDateTime:yyyy-MM-dd HH:mm}."
                };
            }

            return new AvailabilityResponseDTO
            {
                IsAvailable = false,
                AllAvailableTableIds = allAvailableTableIds.ToList(),
                Message = $"No tables available for {numberOfGuests} guests at {requestedDateTime:yyyy-MM-dd HH:mm}."
            };
        }
        #endregion

        #region Booking Mapping
        public static BookingDTO ToBookingDTO(Booking booking)
        {
            return new BookingDTO
            {
                Id = booking.Id,
                CustomerId = booking.CustomerId,
                TableId = booking.TableId,
                StartDateTime = booking.StartDateTime,
                Status = booking.Status,
                CreatedAt = booking.CreatedAt,
                UpdatedAt = booking.UpdatedAt,
                NumberOfGuests = booking.NumberOfGuests,
                CustomerName = booking.Customer?.FullName ?? string.Empty,
                CustomerEmail = booking.Customer?.Email ?? string.Empty,
                CustomerPhoneNumber = booking.Customer?.PhoneNumber ?? string.Empty,
                TableNumber = booking.Table?.TableNumber ?? 0,
                TableCapacity = booking.Table?.SeatingCapacity ?? 0
            };
        }

        public static BookingSummaryDTO ToBookingSummaryDTO(Booking booking)
        {
            return new BookingSummaryDTO
            {
                Id = booking.Id,
                CustomerName = booking.Customer?.FullName ?? string.Empty,
                TableNumber = booking.Table?.TableNumber ?? 0,
                StartDateTime = booking.StartDateTime,
                NumberOfGuests = booking.NumberOfGuests,
                Status = booking.Status
            };
        }
        #endregion

        #region Customer Mapping
        public static CustomerDTO ToCustomerDTO(Customer customer)
        {
            return new CustomerDTO
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                FullName = customer.FullName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
                // Note: TotalBookings would need to be calculated by service layer
            };
        }

        public static CustomerSummaryDTO ToCustomerSummaryDTO(Customer customer, int activeBookings = 0)
        {
            return new CustomerSummaryDTO
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Email = customer.Email,
                Phone = customer.PhoneNumber,
                ActiveBookings = activeBookings
            };
        }
        #endregion

        #region MenuItem Mapping
        public static MenuItemDTO ToMenuItemDTO(MenuItem menuItem)
        {
            return new MenuItemDTO
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Description = menuItem.Description,
                Price = menuItem.Price,
                Category = menuItem.Category,
                IsPopular = menuItem.IsPopular,
                ImageUrl = menuItem.ImageUrl
            };
        }
        #endregion
    }
}

namespace Passenger.Infrastructure.Extenstions
{
    public static class StringExtensions
    {
        public static bool Empty(this string value)
            => string.IsNullOrWhiteSpace(value);
    }
}
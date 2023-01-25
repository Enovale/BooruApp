using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;
using BooruApp.Infrastructure.Models;

namespace BooruApp.Views.Converters
{
    // TODO: This doesn't work for ConvertBack and generally sucks.
    // Need some kind of replacement for SelectedItemPath in Avalonia.
    public class ServerProviderConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is RegisteredServerProvider serverProvider && targetType.IsAssignableTo(typeof(string)))
                return serverProvider.Metadata.DisplayName;
            else if (value is string s && parameter is IEnumerable<RegisteredServerProvider> providerList)
                return providerList.Single(p => p.Metadata.IdName == s);

            throw new ArgumentException($"Convert:Value must be a {nameof(RegisteredServerProvider)}.");
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is RegisteredServerProvider serverProvider && targetType.IsAssignableTo(typeof(string)))
                return serverProvider.Metadata.IdName;
            
            throw new ArgumentException($"ConvertBack:EnumDescription must be an {nameof(RegisteredServerProvider)}.");
        }
    }
}
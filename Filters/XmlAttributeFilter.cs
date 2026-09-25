using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Markup;
using System.Xml;

namespace example_csharp_app.Filters;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class XmlAttribute : ValidationAttribute
{

    public XmlAttribute(): base("Passed string is not XML")
    {}

    /// <summary>
    ///     Override of <see cref="ValidationAttribute.IsValid(object)" />
    /// </summary>
    /// <param name="value">The value to test</param>
    /// <returns>
    ///     Returns <see langword="false" /> if the <paramref name="value" /> is null or an empty string.
    ///     If <see cref="AllowEmptyStrings" /> then <see langword="true" /> is returned for empty strings.
    /// </returns>
    public override bool IsValid(object? value)
    {
        if (value is null || value is not string)
        {
            return false;
        }

        string possiblyXml = ((string)value).Trim();
        if (!possiblyXml.StartsWith('<') && !possiblyXml.EndsWith('>')) 
        {
            return false;
        }
        XmlDocument xml = new XmlDocument();
        try
        {
            xml.LoadXml(possiblyXml);
        }
        catch (XmlException e)
        {
            // logme?
            return false;
        }

        return true;
    }
    
}

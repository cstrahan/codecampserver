namespace CodeCampServer.UI.Models.AutoMap
{
    public interface IFormatterExpression
    {
        void AddFormatter<TValueFormatter>() where TValueFormatter : IValueFormatter;
        void AddFormatter(IValueFormatter formatter);
        void AddFormatExpression(ValueFormatterExpression formatExpression);
        void SkipFormatter<TValueFormatter>() where TValueFormatter : IValueFormatter;
    }

    public interface ICustomFormatterExpression : IFormatterExpression
    {
        IFormatterExpression ForSourceType<TSourceType>();
    }

    public delegate string ValueFormatterExpression(object value, ResolutionContext context);
}
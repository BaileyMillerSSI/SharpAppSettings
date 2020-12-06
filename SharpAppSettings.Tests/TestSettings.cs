namespace SharpAppSettings.Tests
{
    [AppSetting("BindToMe")]
    public class TestSettings
    {
        public string StringValue { get; set; }
        public int NumberValue { get; set; }
        public bool BooleanValue { get; set; }
        public int[] ArrayOfNumbersValue { get; set; }
        public string[] ArrayOfStringsValue { get; set; }
        public Nestedvalue NestedValue { get; set; }

        public class Nestedvalue
        {
            public string StringValue { get; set; }
        }
    }
}
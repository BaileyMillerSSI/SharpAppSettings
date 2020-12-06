# SharpAppSettings

Often times you need a quick and easy way to inject settings into the DI container.
This is fairly easy with the default service collection but requires some code to get started.
This project aims to make injecting settings as easy as adding an attribute and registering with the DI container.

# Getting started:

1. Create your setting classes and mark them with the `AppSettingAttribute`

```
[AppSetting("BindToMe")]
public class TestSettings
{
    public string StringValue { get; set; }
    public int NumberValue { get; set; }
    public bool BooleanValue { get; set; }
    public int[] ArrayOfNumbersValue { get; set; }
    public string[] ArrayOfStringsValue { get; set; }

    ... More public properties ...
}
```

2. Update you `appsettings.json` file with sections you've created classes for

```
{
  "BindToMe": {
    "StringValue": "value",
    "NumberValue": 1,
    "BooleanValue": false,
    "ArrayOfNumbersValue": [ 0, 1, 2, 3 ],
    "ArrayOfStringsValue": [ "a", "b", "c" ],
    "NestedValue": {
      "StringValue": "nested value"
    }
  }
}
```

3. Inside of `Startup.cs` register these settings by calling `AddTypedSettings`

```
serviceCollection.AddTypedSettings(configuration, typeof(ServiceCollectionExtensionsTests).Assembly);
```

4. Start using those options from your DI container

```
public class MyService
{
    private readonly TestSettings _testSettings;

    public MyService(IOptions<TestSettings> testSettings)
    {
        _testSettings = testSettings.Value;
    }
}
```

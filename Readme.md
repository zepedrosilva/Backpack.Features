# Backpack.Features

Backpack.Features is a simple c# library to support [Feature Toggles](http://http://martinfowler.com/bliki/FeatureToggle.html "Feature Toggle, by Martin Fowler").

[![Build status](https://ci.appveyor.com/api/projects/status/k5k2xlb4ud5c06vf/branch/master)](https://ci.appveyor.com/project/JoseSilva/backpack-features/branch/master)

## Usage

### Step 1: Define your features

```
#!csharp
public class SimpleFeature1 : IFeature
{
    public string Name
	{
		get { return "SimpleFeature1"; }
	}

    public bool IsEnabled()
    {
        return false; // default value
    }
}

public class SimpleFeature2 : Feature
{
    public override string Name
    {
        get { return "SimpleFeature2"; }
    }

    public override bool IsEnabled()
    {
        return true; // default value
    }
}
```

You can also add complex features where state depends on a specific circumstance

```
#!csharp
public class StrategyDrivenFeature : Feature<NegativeStrategyStateProvider>
{
    public override string Name
    {
        get { return "StrategyDrivenFeature"; }
    }
}

public class NegativeStrategyStateProvider : IFeatureStateProvider
{
    public bool IsEnabled()
    {
        // Put whatever logic you want here
        // (ex: if the user has a specific role)
        return false;
    }
}
```

### Step 2: Use your favourite IoC container to populate the FeatureContainer

```
#!csharp
public class AutofacBoostrapper : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .AssignableTo<IFeature>()
            .AsSelf()
            .InstancePerLifetimeScope();
    }
}

public class AutofacFeatureProvider : IFeatureProvider
{
    private readonly IContainer _container;

    public AutofacFeatureProvider(IContainer container)
    {
        _container = container;
    }

    public IEnumerable<IFeature> GetAllFeatures()
    {
        return _container.Resolve<IEnumerable<IFeature>>();
    }

    public IFeature GetFeature<TFeature>() where TFeature : class, IFeature
    {
        return _container.Resolve<TFeature>();
    }
}
```

### Step 3: Add the necessary configurations (web.config or app.config) for the feature switchboard

1. If there is no configuration section like the one just below, the internal state of the feature is returned
2. If one does exist, then
	1. If the feature is defined as enabled, then return the internal state of the feature
	2. If the feature is defined as disabled, then return that state immediately

```
#!xml
<?xml version="1.0"?>
<configuration>

    <configSections>
        <section name="backpack.features" type="Backpack.Features.FeatureConfiguration, Backpack.Features"/>
    </configSections>

    <backpack.features>
        <features>

			<!-- feature is disabled explicitly -->
            <add name="SimpleFeature" enabled="false" /> 
            
			<!-- feature is enabled in the configuration -->
			<!-- but its final state will be given by the feature itself -->
			<add name="StrategyDrivenFeature" enabled="true" /> 

        </features>
    </backpack.features>

</configuration>
```

### Step 4: Instantiate the FeatureContainer

```
#!csharp
//
// Setup dependencies (using autofac)
//

var builder = new ContainerBuilder();
builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

var iocContainer = builder.Build();

//
// Initialise the feature container
//

var featureContainer = new FeatureContainer(new AutofacFeatureProvider(iocContainer));
```

### Step 5: Evaluate if the feature is enabled in your code

```
#!csharp
if (featureContainer.OfType<SimpleFeature>().IsEnabled())
{
	...
}

if (featureContainer.OfType<StrategyDrivenFeature>().IsEnabled())
{
	...
}
```
  
## License

The MIT License (MIT)
Copyright (c) 2015 Jose Silva

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANT ABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NON INFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
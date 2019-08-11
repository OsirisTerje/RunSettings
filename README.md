### RunSettings
Item templates for runsettings files

A set of Runsettings solution templates for VS2017 and upwards (means this also support VS2019)   
The runsettings contains separate sections for the adapters.  The settings for the [NUnit Adapter is described here](https://github.com/nunit/docs/wiki/Tips-And-Tricks). 

Right click the solution, choose Add/New Item and choose one of the Runsettings under General, and the runsettings file is added as a solution item, and opened as XML in your editor.  

See http://msdn.microsoft.com/en-us/library/vstudio/jj159530.aspx and https://msdn.microsoft.com/en-gb/library/jj635153.aspx  for information on how to customize the runsettings. 

### Details
A solution item template that creates a set of default runsettings files under Solution Items, and saves you from having to do this manually.  See [http://msdn.microsoft.com/en-us/library/vstudio/jj159530.aspx](http://msdn.microsoft.com/en-us/library/vstudio/jj159530.aspx) for information on how to change the default settings. 

The runsettings file is used to change the code coverage analysis settings for a test run, in particular which files to be included and excluded from analysis,  and also used to set symbol search paths.  It can also set Runconfigurations and testrunparameters, and specific settings for NUnit and MSTest. 

### Parallel execution
It can also enable [parallel run of unit tests](https://blogs.msdn.microsoft.com/visualstudioalm/2016/02/08/parallel-and-context-sensitive-test-execution-with-visual-studio-2015-update-1/).
More information on [MSTest parallel execution](https://devblogs.microsoft.com/devops/mstest-v2-in-assembly-parallel-test-execution) and [more details here](https://github.com/microsoft/testfx-docs/blob/master/RFCs/004-In-Assembly-Parallel-Execution.md) 
More information on [NUnit parallel execution](https://github.com/nunit/docs/wiki/Parallelizable-Attribute)

### Templates
There are 3 templates included, one complete which includes code coverage and parallel run, one with only code coverage, and one with only parallel run.  The latter can also be used as the most simple runsettings template.

See this blogpost for more information :[How to exclude code from code coverage in Visual Studio](http://hermit.no/how-to-exclude-code-from-code-coverage-in-visual-studio-unit-testing-using-runsettings/) 

The current version supperts VS2017 and VS2019.  

#### Versions supporting older Visual Studio versions:

[VS 2015 version ](https://marketplace.visualstudio.com/items?itemName=OsirisTerje.Runsettings)

[VS 2013 version ](/vsgallery/704ebd18-7d60-4341-9224-532f73229c74)

[VS 2012 version ](/vsgallery/601bd207-5889-4935-b101-3ebe1f25aafa)

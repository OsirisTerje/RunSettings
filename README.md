### RunSettings for "real" developers who write unit tests
A set of runsettings item templates for Visual Studio 2019 and 2017. [For earlier versions see the links below] 

The runsettings file is used by all developers who love unit tests.

This template automates the creation of the default runsettings under solution items. 
Never do this manually again!

The runsettings contains separate sections for the adapters.  The settings for the [NUnit Adapter is described here](https://github.com/nunit/docs/wiki/Tips-And-Tricks) and [MSTest adapter is described here](https://docs.microsoft.com/en-us/visualstudio/test/configure-unit-tests-by-using-a-dot-runsettings-file?view=vs-2019#mstest-run-settings). 

### How to use
Right click the solution. Choose Add | New Item and choose one of the Runsettings under General. Observe that the runsettings file was added as a Solution Item.

Note that if you drop the name, and just let the file be named ```.runsettings``` it will be automatically picked up by Visual Studio. 

### Details
The runsettings file is used by all developers who love unit tests.

This template automates the creation of the default runsettings under solution items. Never do this manually again!   

Also used to change the code coverage analysis settings for a test run. Set which files should be included and excluded from analysis, and also set symbol search paths.  Also set Runconfigurations and testrunparameters, and specific settings for NUnit and MSTest. 

Learn how to configure and customize runsettings:
- https://docs.microsoft.com/en-us/visualstudio/test/configure-unit-tests-by-using-a-dot-runsettings-file?view=vs-2019  
- https://docs.microsoft.com/en-us/visualstudio/test/customizing-code-coverage-analysis?view=vs-2019 

Enjoy!

### Parallel execution
It can also enable [parallel run of unit tests](https://blogs.msdn.microsoft.com/visualstudioalm/2016/02/08/parallel-and-context-sensitive-test-execution-with-visual-studio-2015-update-1/).
More information on [MSTest parallel execution](https://devblogs.microsoft.com/devops/mstest-v2-in-assembly-parallel-test-execution) and [more details here](https://github.com/microsoft/testfx-docs/blob/master/RFCs/004-In-Assembly-Parallel-Execution.md) 
More information on [NUnit parallel execution](https://github.com/nunit/docs/wiki/Parallelizable-Attribute)

### Templates
There are 3 templates included, one complete which includes code coverage and parallel run, one with only code coverage, and one with only parallel run.  The latter can also be used as the most simple runsettings template.

For more information see this blog post: [How to exclude code from code coverage in Visual Studio](http://hermit.no/how-to-exclude-code-from-code-coverage-in-visual-studio-unit-testing-using-runsettings/) 

### Why are there no settings for XUnit included

XUnit does not support the use of a runsettings file, but use environment variables instead. See the answer in this [SO post](https://stackoverflow.com/questions/54977531/how-to-read-runsettings-test-parameter-in-xunit-fixture) and this [github issue](https://github.com/xunit/xunit/issues/1439.)

#### Older Visual Studio versions:

[VS 2015 version ](https://marketplace.visualstudio.com/items?itemName=OsirisTerje.Runsettings)

[VS 2013 version ](/vsgallery/704ebd18-7d60-4341-9224-532f73229c74)

[VS 2012 version ](/vsgallery/601bd207-5889-4935-b101-3ebe1f25aafa)

#### Credits
Thanks to [Adam Cogan](https://adamcogan.com/) for your help! 

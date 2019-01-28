


###  Build it

Just run the command 

```
b.cmd
```


Result is in the output bin\release folder

### Details


The b command will start a cake script which does the steps below


See https://msdn.microsoft.com/en-us/library/tsyyf0yh.aspx, last section, on zip files

1. Modify the runsettings files
2. Copy the file into the zip file in the same folder
3. Copy the zip file into the ItemTemplates folder
4. Build a release version to get the vsix

Build, in release.




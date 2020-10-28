
`dotnet build`

`dotnet test /p:CoverletOutput=../CoverageResults/ /p:MergeWith=../CoverageResults/coverage.json "/p:CoverletOutputFormat=\"opencover,json,cobertura\"" /p:CollectCoverage=true -m:1`

`reportgenerator "-reports:CoverageResults\coverage.cobertura.xml" "-targetdir:CoverageResults" -reporttypes:Html`

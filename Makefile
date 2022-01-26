all : init clean restore build tests

c = $(CURDIR)/coverage
n = $(CURDIR)/nupkgs

init:
	dotnet tool install dotnet-reportgenerator-globaltool 
clean:
	dotnet clean

restore:
	dotnet restore

build: 
	dotnet build

tests:
	rmdir /s /q "$c/"
	dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=$c/coverage.xml

report:
	reportgenerator -reports:"$c/coverage.xml" -targetdir:"$c" -reporttypes:Html
pack:
	dotnet pack --output $n
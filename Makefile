all : init clean restore build tests report pack

c = $(CURDIR)/coverage
n = $(CURDIR)/nupkgs

init:
	@echo "INSTALLING TOOLS"

	dotnet new tool-manifest --force

	dotnet tool install dotnet-reportgenerator-globaltool 
	
clean:
	@echo "CLEANING"

	dotnet clean

restore:
	
	@echo "RESTORING"

	dotnet restore

build: 
	@echo "BUILDING"
	dotnet build

tests:

	@echo "TESTING"

ifneq ($(wildcard $c/),)
	rmdir /s /q "$c/"
endif

	dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=$c/coverage.xml

report:

	@echo "GENERATING COVERAGE REPORT"

	dotnet reportgenerator -reports:"$c/coverage.xml" -targetdir:"$c/" -reporttypes:Html
pack:

	@echo "GENERATING NUGET PACKAGES"

ifneq ($(wildcard $n/),)
	rmdir /s /q "$n/"
endif	

	dotnet pack --output $n
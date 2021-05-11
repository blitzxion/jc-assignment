# JumpCloud Assignment

A library that adds, validates, tracks, and produces statistics on enqueued actions.

Content here is based off the information found within the following [JumpCloud document](./Software%20Engineer%20-%20Backend%20Assignment.pdf)

## Requirements

The development environment must have the following:

- Windows 10
- [Microsoft Visual Studio 2019 Community Edition](https://visualstudio.microsoft.com/downloads/) or greater
- [.NET 4.8 SDK](https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net48-developer-pack-offline-installer)
- PowerShell
- Active internet connection

## Getting Started

Clone this repo and enter the root directory:
```shell
git clone https://github.com/blitzxion/jc-assignment
cd jc-assignment
```

### Building

This project can be built using Visual Studio (recommended) or by running the following script:
```powershell
# PowerShell or...
.\build.ps1

# CMD
build.bat
```

## Testing

All tests can be executed by running the following script:

```powershell
# PowerShell or...
.\runtests.ps1

# CMD
runtests.bat
```

The testing framework utilized is [XUnit](https://xunit.net/). XUnit provides a simplified, open source, community-focused unit testing tool for .NET Framework.

## Usage

### Library

The `JumpCloudAssignment.Lib` library provides a public class that exposes two methods for use.

#### ActionService
```csharp
public class ActionService { /*...* / }
```
- Parameters
  - none

This is the core class for this library. This class provides the methods and properties needed to add actions and get statistics from those added actions.

#### AddAction
```csharp
public string AddAction(string action);
```
- Paramaters
  - `string` action
- Returns: `string`

This method is used to add an action to the service's internal collection. The action provided must be a valid JSON string that represents a valid action object.

A valid action JSON string would look like:
```json
{ "action":"jump", "time":120 }
// or
{ "action":"run", "time":20 }
```

In the event that an error occurs when attempting to add the action, this method will return a `string` that describes the failure. Providing bad JSON, missing required properties, or invalid values will result in an error being returned. 

#### GetStats
```csharp
public string GetStats();
```
- Paramaters
  - none
- Returns: `string`

This method is used to generate a statistical output of all collected actions in the service. The results will be a JSON array of unique actions and their average times.

Example output would be:
```json
[{"action":"jump","avg":330}, {"action":"run","avg":40}]
```

## Example Usage

The following example demonstrates the basic use of this library:
```csharp
using JumpCloudAssignment.Lib;

var service = new ActionService;

var add1 = service.AddAction("{\"action\":\"jump\",\"time\":100}");
var add2 = service.AddAction("{\"action\":\"run\",\"time\":75}");
var add3 = service.AddAction("{\"action\":\"jump\",\"time\":200}");
// all these should be empty, no issue with the JSON

var stats = service.GetStats();
/* returns: 
    [
        {"action":"jump", "avg":150}, 
        {"action":"run", "avg":75}
    ]
*/

```

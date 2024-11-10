# RequestParameters
## about_DataverseRequestParameters

# SHORT DESCRIPTION
Describes the parameters that can be used with Dataverse request cmdlets.

# LONG DESCRIPTION
Dataverse provides a set of optional parameters or request header values a developer of a client application can use to modify the behavior of individual requests.

The following list displays the common parameters.

- BypassPowerAutomateFlows
- BypassSynchronousLogic
- FailOnDuplicateDetection
- Partition
- SharedTag
- Solution

## Request parameter descriptions

### -BypassPowerAutomateFlows
When bulk data operations occur that trigger flows, Dataverse creates system jobs to execute the flows. When the number of system jobs is very large, it may cause performance issues for the system. If this occurs, you can choose to bypass triggering the flows by using the parameter.

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BypassSynchronousLogic
Synchronous logic must be applied during the transaction and can significantly impact performance of individual operations. When performing bulk operations, the additional time for these individual operations can increase the time required. Use the parameter when you want to improve performance while performing bulk data operations.

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FailOnDuplicateDetection
If you want to have Dataverse throw an error when a new record you create or a record you update matches the duplicate detection rules for another record.

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Partition
When using elastic tables with a partitioning strategy, you can pass a unique string value with the partitionid  to access non-relational table data within a storage partition.

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SharedTag
Use the parameter to include a shared variable value that is accessible within a plug-in. This extra information allows a plug-in to apply logic that depends on the client application.

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Solution
When you perform data operations on a solution component, you can associate it with a solution by specifying the solution unique name.

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

# SEE ALSO
(Use optional parameters)[https://learn.microsoft.com/en-us/power-apps/developer/data-platform/optional-parameters]

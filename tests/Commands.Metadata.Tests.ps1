BeforeAll {
	$TestDirectory = Split-Path $PSCommandPath -Parent
	$RootDirectory = Split-Path $TestDirectory -Parent

	$ModuleDirectory = Resolve-Path -Path (Join-Path -Path $RootDirectory -ChildPath '.\dist\AMSoftware.Dataverse.PowerShell\')
	$ModulePath = Join-Path -Path $ModuleDirectory -ChildPath 'AMSoftware.Dataverse.PowerShell.psd1'

	Import-Module -Name $ModulePath -Scope Local -Force

	Connect-DataverseEnvironment -EnvironmentUrl $env:DATAVERSEURL_TEST -ClientId $env:DATAVERSECLIENTID_TEST -ClientSecret (ConvertTo-SecureString -String $env:DATAVERSECLIENTSECRET_TEST -AsPlainText -Force)
}

AfterAll {
	Remove-Module 'AMSoftware.Dataverse.PowerShell'
}

Describe 'Commands.Metadata' -Tag 'Integration' {
	BeforeAll {
		$postfix = [datetime]::Now.Ticks.ToString()
	}

	Context 'Get-DataverseTable' {
		It 'Get Table by Id' {
			$accountTable = Get-DataverseTable -Name 'account'

			$output = $accountTable.MetadataId | Get-DataverseTable 

			$output | Should -HaveCount 1
			$output.LogicalName | Should -Be 'account' 
		}

		It 'Get Table by ETC' {
			$output = Get-DataverseTable -TypeCode 1

			$output | Should -HaveCount 1
			$output.LogicalName | Should -Be 'account' 
		}

		It 'Get Table with Filters' {
			$output = Get-DataverseTable -Name 'account' -Exclude 'contact' -Type Standard 

			$output | Should -HaveCount 1
			$output.LogicalName | Should -Be 'account' 
		}

		It 'Get All Tables' {
			$output = Get-DataverseTable

			$output | Select-Object -First 50 | Should -HaveCount 50
		}

		It 'Get Intersect Tables' {
			$output = Get-DataverseTable -Intersects

			$output | Select-Object -First 10 | Should -HaveCount 10
		}
	}

	Context 'Get-DataverseChoice' {
		It 'Get Choice By Id' {
			$choice = Get-DataverseChoice | Select-Object -First 1

			$output = $choice | Get-DataverseChoice
			$output | Should -HaveCount 1
		}

		It 'Get all Choices' {
			$output = Get-DataverseChoice
			$output | Select-Object -First 10 | Should -HaveCount 10
		}

		It 'Get filtered Choices' {
			$output = Get-DataverseChoice -Name '*' -Exclude 'a*'
			$output | Select-Object -First 10 | Should -HaveCount 10
		}
	}

	Context 'Get-DataverseRelationship' {
		It 'Get Relationship By Id' {
			$relationship = Get-DataverseRelationship -Table 'account' | Select-Object -First 1

			$output = $relationship | Get-DataverseRelationship
			$output | Should -HaveCount 1
		}

		It 'Get relationship by Name' {
			$relationship = Get-DataverseRelationship -Table 'account' | Select-Object -First 1

			$output = Get-DataverseRelationship -Name $relationship.SchemaName
			$output | Should -HaveCount 1
		}

		It 'Get Table Relationships' {
			$output = Get-DataverseRelationship -Table 'account' -RelatedTable 'contact'
			
			$output | Should -HaveCount 3
					
		}
	}

	Context 'Get-DataverseColumn' {
		It 'Get Column by Name' {
			$output = Get-DataverseColumn -Table 'account' -Name 'accountnumber'

			$output | Should -HaveCount 1
			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.StringAttributeMetadata'
			$output.LogicalName | Should -Be 'accountnumber'
		}

		It 'Get Lookup Columns' {
			$output = Get-DataverseColumn -Table 'account' -Type Lookup

			$output | Select-Object -First 3 | Should -HaveCount 3
			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.LookupAttributeMetadata'
		}

		It 'Get Column By Id' {
			$column = Get-DataverseColumn -Table 'account' -Name 'accountnumber'
			$output = $column | Get-DataverseColumn

			$output | Should -HaveCount 1
			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.StringAttributeMetadata'
			$output.LogicalName | Should -Be 'accountnumber'
		}
	}

	Context 'Add-DataverseChoice' {
		It 'Add Choice from values' {
			$options = @(
				[dvchoiceoption]::new([dvlabel]'Label 1', 876999001)
				[dvchoiceoption]::new([dvlabel]'Label 2', 876999002)
			)

			$output = Add-DataverseChoice `
				-Name "ams_testoption$($postfix)" `
				-DisplayName 'Test Option' `
				-Description 'Test Option Description' `
				-ExternalName 'Test Option External Name' `
				-Options $options

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.OptionSetMetadata'
			$output.Name | Should -Be "ams_testoption$($postfix)"
			$output.Options | Should -HaveCount 2
		}
	}

	Context 'Add-DataverseTableKey' {
		It 'Add Table Keys from Values' {
			$output = Add-DataverseTableKey `
				-Table 'account' `
				-Name "ams_accountnumber$($postfix)" `
				-DisplayName 'Accountnumber' `
				-Columns 'accountnumber'

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.EntityKeyMetadata'
			$output.LogicalName | Should -Be "ams_accountnumber$($postfix)"
		}
	}

	Context 'Add-DataverseTable' {
		It 'Add Standard Table' {
			$output = Add-DataverseTable `
				-Name "ams_testtable$($postfix)"`
				-DisplayName 'Test Table' `
				-PluralName 'Test Tables' `
				-Description 'Test Table Description' `
				-OwnershipType Organization `
				-HasAttachments `
				-IsActivityParty `
				-TrackChanges `
				-ColumnName 'ams_name' `
				-ColumnDisplayName 'Name' `
				-ColumnDescription 'Name Description' `
				-ColumnLength 104 `
				-ColumnRequired Recommended

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.EntityMetadata'
			$output.LogicalName | Should -Be "ams_testtable$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test Table'
			$output.DisplayCollectionName.ToString() | Should -Be 'Test Tables'
			$output.Description.ToString() | Should -Be 'Test Table Description'
			$output.OwnershipType | Should -Be OrganizationOwned
			$output.HasNotes | Should -BeTrue
			$output.HasActivities | Should -BeTrue
			$output.ChangeTrackingEnabled | Should -BeTrue
			$output.TableType | Should -Be 'Standard'
			$output.PrimaryNameAttribute | Should -Be 'ams_name'

			$column = $output.Attributes | Where-Object { $_.LogicalName -eq $output.PrimaryNameAttribute } 
			$column | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.StringAttributeMetadata'

			$column.LogicalName | Should -Be 'ams_name'
			$column.DisplayName.ToString() | Should -Be 'Name'
			$column.Description.ToString() | Should -Be 'Name Description'
			$column.MaxLength | Should -Be 104
			$column.RequiredLevel.Value | Should -Be Recommended
		}

		It 'Add Standard Table with Defaults' {
			$output = Add-DataverseTable `
				-Name "ams_testdefault$($postfix)"`
				-DisplayName 'Test Table' `
				-PluralName 'Test Tables' `
				-ColumnName 'ams_name' `
				-ColumnDisplayName 'Name'

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.EntityMetadata'
			$output.LogicalName | Should -Be "ams_testdefault$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test Table'
			$output.DisplayCollectionName.ToString() | Should -Be 'Test Tables'
			$output.OwnershipType | Should -Be UserOwned
			$output.HasNotes | Should -BeFalse
			$output.HasActivities | Should -BeFalse
			$output.ChangeTrackingEnabled | Should -BeFalse
			$output.TableType | Should -Be 'Standard'
			$output.PrimaryNameAttribute | Should -Be 'ams_name'
	
			$column = $output.Attributes | Where-Object { $_.LogicalName -eq $output.PrimaryNameAttribute } 
			$column | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.StringAttributeMetadata'
	
			$column.LogicalName | Should -Be 'ams_name'
			$column.DisplayName.ToString() | Should -Be 'Name'
			$column.MaxLength | Should -Be 100
			$column.RequiredLevel.Value | Should -BeIn ApplicationRequired
		}

		It 'Add Activity Table' {
			$output = Add-DataverseTable `
				-Activity `
				-Name "ams_testactivity$($postfix)"`
				-DisplayName 'Test Activity' `
				-PluralName 'Test Activities' `
				-Description 'Test Activity Description' `
				-TrackChanges `
				-HideFromMenu

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.EntityMetadata'
			$output.LogicalName | Should -Be "ams_testactivity$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test Activity'
			$output.DisplayCollectionName.ToString() | Should -Be 'Test Activities'
			$output.Description.ToString() | Should -Be 'Test Activity Description'
			$output.OwnershipType | Should -Be UserOwned
			$output.HasNotes | Should -BeTrue
			$output.HasActivities | Should -BeFalse
			$output.ChangeTrackingEnabled | Should -BeTrue
			$output.TableType | Should -Be 'Standard'
			$output.PrimaryNameAttribute | Should -Be 'subject'
			$output.IsActivity | Should -BeTrue

			$column = $output.Attributes | Where-Object { $_.LogicalName -eq $output.PrimaryNameAttribute } 
			$column | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.StringAttributeMetadata'

			$column.LogicalName | Should -Be 'subject'
			$column.DisplayName.ToString() | Should -Be 'Subject'
			$column.MaxLength | Should -Be 400
			$column.RequiredLevel.Value | Should -Be ApplicationRequired
		}

		It 'Add Elastic Table' {
			$output = Add-DataverseTable `
				-Elastic `
				-Name "ams_testelastic$($postfix)"`
				-DisplayName 'Test Elastic' `
				-PluralName 'Test Elastics' `
				-Description 'Test Elastic Description' `
				-OwnershipType User `
				-TrackChanges `
				-ColumnName 'ams_name' `
				-ColumnDisplayName 'Name' `
				-ColumnDescription 'Name Description' `
				-ColumnLength 104 `
				-ColumnRequired Recommended

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.EntityMetadata'
			$output.LogicalName | Should -Be "ams_testelastic$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test Elastic'
			$output.DisplayCollectionName.ToString() | Should -Be 'Test Elastics'
			$output.Description.ToString() | Should -Be 'Test Elastic Description'
			$output.OwnershipType | Should -Be UserOwned
			$output.HasNotes | Should -BeFalse
			$output.HasActivities | Should -BeFalse
			$output.ChangeTrackingEnabled | Should -BeTrue
			$output.TableType | Should -Be 'Elastic'
			$output.PrimaryNameAttribute | Should -Be 'ams_name'
			$output.IsActivity | Should -BeFalse

			$column = $output.Attributes | Where-Object { $_.LogicalName -eq $output.PrimaryNameAttribute } 
			$column | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.StringAttributeMetadata'

			$column.LogicalName | Should -Be 'ams_name'
			$column.DisplayName.ToString() | Should -Be 'Name'
			$column.Description.ToString() | Should -Be 'Name Description'
			$column.MaxLength | Should -Be 104
			$column.RequiredLevel.Value | Should -Be Recommended
		}

		It 'Add Virtual Table' {
			$output = Add-DataverseTable `
				-Virtual `
				-Name "ams_testvirtual$($postfix)"`
				-DisplayName 'Test Virtual' `
				-PluralName 'Test Virtuals' `
				-Description 'Test Virtual Description' `
				-HasAttachments `
				-IsActivityParty `
				-ColumnName 'ams_name' `
				-ColumnDisplayName 'Name' `
				-ColumnDescription 'Name Description' `
				-ColumnLength 104 `
				-ColumnRequired Recommended `
				-ExternalName 'testvirtual' `
				-ExternalPluralName 'testvirtuals' `
				-ColumnExternalName 'name'

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.EntityMetadata'
			$output.LogicalName | Should -Be "ams_testvirtual$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test Virtual'
			$output.DisplayCollectionName.ToString() | Should -Be 'Test Virtuals'
			$output.Description.ToString() | Should -Be 'Test Virtual Description'
			$output.OwnershipType | Should -Be OrganizationOwned
			$output.HasNotes | Should -BeTrue
			$output.HasActivities | Should -BeTrue
			$output.ChangeTrackingEnabled | Should -BeFalse
			$output.TableType | Should -Be 'Virtual'
			$output.PrimaryNameAttribute | Should -Be 'ams_name'
			$output.IsActivity | Should -BeFalse
			$output.ExternalName | Should -Be 'testvirtual'
			$output.ExternalCollectionName | Should -Be 'testvirtuals'
			$output.DataProviderId | Should -Be '7015A531-CC0D-4537-B5F2-C882A1EB65AD'

			$column = $output.Attributes | Where-Object { $_.LogicalName -eq $output.PrimaryNameAttribute } 
			$column | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.StringAttributeMetadata'
			$column.LogicalName | Should -Be 'ams_name'
			$column.DisplayName.ToString() | Should -Be 'Name'
			$column.Description.ToString() | Should -Be 'Name Description'
			$column.MaxLength | Should -Be 104
			$column.RequiredLevel.Value | Should -Be Recommended
			$column.ExternalName | Should -Be 'name'
		}
	}

	Context 'Add-DataverseColumn' {
		It 'Add Column with Defaults' {
			$output = Add-DataverseColumn `
				-Table 'account' `
				-Name "ams_testdefaults$($postfix)" `
				-Type String `
				-DisplayName 'Test Defaults'
		
			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.StringAttributeMetadata'
			$output.LogicalName | Should -Be "ams_testdefaults$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test Defaults'
			$output.RequiredLevel.Value | Should -Be ApplicationRequired
			$output.ExternalName | Should -BeNullOrEmpty
			$output.IsValidForAdvancedFind.Value | Should -BeFalse 
			$output.IsAuditEnabled.Value | Should -BeFalse
			$output.IsSecured | Should -BeFalse 
			$output.SourceType | Should -Be 0
			$output.Format | Should -Be Text
			$output.ImeMode | Should -Be Auto 
			$output.MaxLength | Should -Be 100
		}

		It 'Add Formula Column' {
			$output = Add-DataverseColumn `
				-Table 'account' `
				-Name "ams_testformula$($postfix)" `
				-Type String `
				-DisplayName 'Test Formula'`
				-Source Formula

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.StringAttributeMetadata'
			$output.LogicalName | Should -Be "ams_testformula$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test Formula'
			$output.RequiredLevel.Value | Should -Be None
			$output.ExternalName | Should -BeNullOrEmpty
			$output.IsValidForAdvancedFind.Value | Should -BeFalse 
			$output.IsAuditEnabled.Value | Should -BeFalse
			$output.IsSecured | Should -BeFalse
			$output.SourceType | Should -Be 3
		}

		It 'Add String Column' {
			$output = Add-DataverseColumn `
				-Table 'account' `
				-Name "ams_teststring$($postfix)" `
				-Type String `
				-DisplayName 'Test String' `
				-Description 'Test String Description' `
				-Required Recommended `
				-ExternalName 'teststring' `
				-Searchable `
				-Auditing `
				-ColumnSecurity `
				-Format Url `
				-ImeMode Auto `
				-MaxLength 155

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.StringAttributeMetadata'
			$output.LogicalName | Should -Be "ams_teststring$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test String'
			$output.Description.ToString() | Should -Be 'Test String Description'
			$output.RequiredLevel.Value | Should -Be Recommended
			$output.ExternalName | Should -Be 'teststring'
			$output.IsValidForAdvancedFind.Value | Should -BeTrue 
			$output.IsAuditEnabled.Value | Should -BeTrue
			$output.IsSecured | Should -BeTrue 
			$output.Format | Should -Be Url 
			$output.ImeMode | Should -Be Auto 
			$output.MaxLength | Should -Be 155
		}

		It 'Add Boolean Column' {
			$output = Add-DataverseColumn `
				-Table 'account' `
				-Name "ams_testboolean$($postfix)" `
				-Type Boolean `
				-DisplayName 'Test Boolean' `
				-DefaultValue $true

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.BooleanAttributeMetadata'
			$output.LogicalName | Should -Be "ams_testboolean$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test Boolean'

			$output.DefaultValue | Should -BeTrue
			$output.OptionSet.FalseOption.Value | Should -Be 0
			$output.OptionSet.TrueOption.Value | Should -Be 1
		}

		It 'Add DateTime Column' {
			$output = Add-DataverseColumn `
				-Table 'account' `
				-Name "ams_testdatetime$($postfix)" `
				-Type DateTime `
				-DisplayName 'Test DateTime' `
				-Format DateOnly `
				-ImeMode Auto `
				-Behavior DateOnly

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.DateTimeAttributeMetadata'
			$output.LogicalName | Should -Be "ams_testdatetime$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test DateTime'

			$output.Format | Should -Be DateOnly
			$output.DateTimeBehavior.Value | Should -Be DateOnly
			$output.ImeMode | Should -Be Auto
		}

		It 'Add Decimal Column' {
			$output = Add-DataverseColumn `
				-Table 'account' `
				-Name "ams_testdecimal$($postfix)" `
				-Type Decimal `
				-DisplayName 'Test Decimal' `
				-MinValue 1.0 `
				-MaxValue 100.0 `
				-Precision 2 `
				-ImeMode Auto

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.DecimalAttributeMetadata'
			$output.LogicalName | Should -Be "ams_testdecimal$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test Decimal'

			$output.MinValue | Should -Be 1.0
			$output.MaxValue | Should -Be 100.0
			$output.Precision | Should -Be 2
			$output.ImeMode | Should -Be Auto
		}

		It 'Add Double Column' {
			$output = Add-DataverseColumn `
				-Table 'account' `
				-Name "ams_testdouble$($postfix)" `
				-Type Double `
				-DisplayName 'Test Double' `
				-MinValue 1.0 `
				-MaxValue 100.0 `
				-Precision 2 `
				-ImeMode Auto

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.DoubleAttributeMetadata'
			$output.LogicalName | Should -Be "ams_testdouble$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test Double'

			$output.MinValue | Should -Be 1.0
			$output.MaxValue | Should -Be 100.0
			$output.Precision | Should -Be 2
			$output.ImeMode | Should -Be Auto
		}

		It 'Add Integer Column' {
			$output = Add-DataverseColumn `
				-Table 'account' `
				-Name "ams_testinteger$($postfix)" `
				-Type Integer `
				-DisplayName 'Test Integer' `
				-MinValue 1 `
				-MaxValue 100 `
				-Format Duration

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.IntegerAttributeMetadata'
			$output.LogicalName | Should -Be "ams_testinteger$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test Integer'

			$output.MinValue | Should -Be 1
			$output.MaxValue | Should -Be 100
			$output.Format | Should -Be Duration
		}

		It 'Add BigInt Column' {
			$output = Add-DataverseColumn `
				-Table 'account' `
				-Name "ams_testbigint$($postfix)" `
				-Type BigInt `
				-DisplayName 'Test BigInt'

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.BigIntAttributeMetadata'
			$output.LogicalName | Should -Be "ams_testbigint$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test BigInt'
		}

		It 'Add Money Column' {
			$output = Add-DataverseColumn `
				-Table 'account' `
				-Name "ams_testmoney$($postfix)" `
				-Type Money `
				-DisplayName 'Test Money' `
				-MinValue 1.0 `
				-MaxValue 100.0 `
				-Precision 2 `
				-PrecisionSource Currency `
				-ImeMode Auto

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.MoneyAttributeMetadata'
			$output.LogicalName | Should -Be "ams_testmoney$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test Money'

			$output.MinValue | Should -Be 1.0
			$output.MaxValue | Should -Be 100.0
			$output.Precision | Should -Be 2
			$output.PrecisionSource | Should -Be 2
			$output.ImeMode | Should -Be Auto
		}

		It 'Add Memo Column' {
			$output = Add-DataverseColumn `
				-Table 'account' `
				-Name "ams_testmemo$($postfix)" `
				-Type Memo `
				-DisplayName 'Test Memo' `
				-Format Text `
				-MaxLength 4000 `
				-ImeMode Auto

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.MemoAttributeMetadata'
			$output.LogicalName | Should -Be "ams_testmemo$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test Memo'

			$output.Format | Should -Be Text
			$output.MaxLength | Should -Be 4000
			$output.ImeMode | Should -Be Auto
		}

		It 'Add File Column' {
			$output = Add-DataverseColumn `
				-Table 'account' `
				-Name "ams_testfile$($postfix)" `
				-Type File `
				-DisplayName 'Test File' `
				-MaxSizeInKB 10240

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.FileAttributeMetadata'
			$output.LogicalName | Should -Be "ams_testfile$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test File'

			$output.MaxSizeInKB | Should -Be 10240
		}

		It 'Add Image Column' {
			$output = Add-DataverseColumn `
				-Table 'account' `
				-Name "ams_testimage$($postfix)" `
				-Type Image `
				-DisplayName 'Test Image' `
				-MaxSizeInKB 10240 `
				-IsPrimaryImage `
				-CanStoreFullImage

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.ImageAttributeMetadata'
			$output.LogicalName | Should -Be "ams_testimage$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test Image'

			$output.MaxSizeInKB | Should -Be 10240
			$output.IsPrimaryImage | Should -BeTrue
			$output.CanStoreFullImage | Should -BeTrue
		}

		It 'Add Local Picklist Column' {
			$options = @(
				[dvchoiceoption]::new([dvlabel]'Monday', 1)
				[dvchoiceoption]::new([dvlabel]'Tuesday', 2)
				[dvchoiceoption]::new([dvlabel]'Wednesday', 3)
				[dvchoiceoption]::new([dvlabel]'Thursday', 4)
				[dvchoiceoption]::new([dvlabel]'Friday', 5)
			)

			$output = Add-DataverseColumn `
				-Table 'account' `
				-Name "ams_testlocalpicklist$($postfix)" `
				-Type Picklist `
				-DisplayName 'Test Picklist' `
				-DefaultValue 5 `
				-Options $options

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.PicklistAttributeMetadata'
			$output.LogicalName | Should -Be "ams_testlocalpicklist$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test Picklist'

			$output.DefaultFormValue | Should -Be 5
			$output.OptionSet.IsGlobal | Should -BeFalse
			$output.OptionSet.Options | Should -HaveCount 5
		}

		It 'Add Synced Picklist Column' {
			$output = Add-DataverseColumn `
				-Table 'account' `
				-Name "ams_testsyncedpicklist$($postfix)" `
				-Type Picklist `
				-DisplayName 'Test Picklist' `
				-DefaultValue 5 `
				-GlobalOptionsetName 'yearlymonth_options'

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.PicklistAttributeMetadata'
			$output.LogicalName | Should -Be "ams_testsyncedpicklist$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test Picklist'

			$output.DefaultFormValue | Should -Be 5
			$output.OptionSet.Name | Should -Be 'yearlymonth_options'
			$output.OptionSet.IsGlobal | Should -BeTrue
		}

		It 'Add Local Multi Select Picklist Column' {
			$options = @(
				[dvchoiceoption]::new([dvlabel]'Monday', 1)
				[dvchoiceoption]::new([dvlabel]'Tuesday', 2)
				[dvchoiceoption]::new([dvlabel]'Wednesday', 3)
				[dvchoiceoption]::new([dvlabel]'Thursday', 4)
				[dvchoiceoption]::new([dvlabel]'Friday', 5)
			)

			$output = Add-DataverseColumn `
				-Table 'account' `
				-Name "ams_testlocalmultipicklist$($postfix)" `
				-Type MultiSelectPicklist `
				-DisplayName 'Test Multi Picklist' `
				-Options $options

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.MultiSelectPicklistAttributeMetadata'
			$output.LogicalName | Should -Be "ams_testlocalmultipicklist$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test Multi Picklist'

			$output.OptionSet.IsGlobal | Should -BeFalse
			$output.OptionSet.Options | Should -HaveCount 5
		}

		It 'Add Synced Multi Select Picklist Column' {
			$output = Add-DataverseColumn `
				-Table 'account' `
				-Name "ams_testsyncedmultipicklist$($postfix)" `
				-Type MultiSelectPicklist `
				-DisplayName 'Test Multi Picklist' `
				-GlobalOptionsetName 'yearlymonth_options'

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.MultiSelectPicklistAttributeMetadata'
			$output.LogicalName | Should -Be "ams_testsyncedmultipicklist$($postfix)"
			$output.DisplayName.ToString() | Should -Be 'Test Multi Picklist'

			$output.OptionSet.Name | Should -Be 'yearlymonth_options'
			$output.OptionSet.IsGlobal | Should -BeTrue
		}
	}

	Context 'Add-DataverseRelationship' {
		It 'Add Many-to-Many' {
			$output = Add-DataverseRelationship `
				-Table 'account' `
				-RelatedTable 'contact' `
				-Name "ams_testmany$($postfix)" `
				-Intersect "ams_account_contact_intersect$($postfix)" `
				-Searchable 

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.ManyToManyRelationshipMetadata'
			$output.Entity1LogicalName | Should -Be 'account'
			$output.Entity2LogicalName | Should -Be 'contact'
			$output.SchemaName = "ams_testmany$($postfix)"
			$output.IsValidForAdvancedFind | Should -BeTrue
			
			{ Get-DataverseTable -Name "ams_account_contact_intersect$($postfix)" -ErrorAction Stop } | Should -Not -Throw
		}

		It 'Add Many-to-One' {
			$output = Add-DataverseRelationship `
				-Table 'account' `
				-ColumnName "ams_testlookup$($postfix)" `
				-ColumnDisplayName 'Test Lookup' `
				-ColumnRequired Recommended `
				-ColumnDescription 'Test Lookup Description' `
				-RelatedTable 'contact' `
				-Name "ams_testlookup$($postfix)" `
				-Searchable

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.OneToManyRelationshipMetadata'
			$output.ReferencingEntity | Should -Be 'account'
			$output.ReferencedEntity | Should -Be 'contact'
			$output.SchemaName = "ams_testlookup$($postfix)"
			$output.IsValidForAdvancedFind | Should -BeTrue
			
			$column = Get-DataverseColumn -Table 'account' -Name "ams_testlookup$($postfix)"
			$column | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.LookupAttributeMetadata'
			$column.LogicalName | Should -Be "ams_testlookup$($postfix)"
			$column.DisplayName.ToString() | Should -Be 'Test Lookup'
			$column.Description.ToString() | Should -Be 'Test Lookup Description'
			$column.RequiredLevel.Value | Should -Be Recommended
			$column.IsValidForAdvancedFind.Value | Should -BeTrue 
			$column.IsAuditEnabled.Value | Should -BeFalse
			$column.IsSecured | Should -BeFalse 
			$column.Targets | Should -Be @('contact')
		}

		It 'Add Customer' {
			$output = Add-DataverseRelationship `
				-Table 'account' `
				-ColumnName "ams_testcustomer$($postfix)" `
				-ColumnDisplayName 'Test Customer' `
				-ColumnRequired Recommended `
				-ColumnDescription 'Test Customer Description' `
				-AccountName "ams_account_customeraccount$($postfix)" `
				-ContactName "ams_account_customercontact$($postfix)" `
				-Searchable

			$output | Should -HaveCount 2

			$output[0] | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.OneToManyRelationshipMetadata'
			$output[0].ReferencingEntity | Should -Be 'account'
			$output[0].ReferencedEntity | Should -Be 'account'
			$output[0].SchemaName | Should -Be "ams_account_customeraccount$($postfix)"
			$output[0].IsValidForAdvancedFind | Should -BeTrue
			
			$output[1] | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.OneToManyRelationshipMetadata'
			$output[1].ReferencingEntity | Should -Be 'account'
			$output[1].ReferencedEntity | Should -Be 'contact'
			$output[1].SchemaName | Should -Be "ams_account_customercontact$($postfix)"
			$output[1].IsValidForAdvancedFind | Should -BeTrue

			$column = Get-DataverseColumn -Table 'account' -Name "ams_testcustomer$($postfix)"
			$column | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.LookupAttributeMetadata'
			$column.LogicalName | Should -Be "ams_testcustomer$($postfix)"
			$column.DisplayName.ToString() | Should -Be 'Test Customer'
			$column.Description.ToString() | Should -Be 'Test Customer Description'
			$column.RequiredLevel.Value | Should -Be Recommended
			$column.IsValidForAdvancedFind.Value | Should -BeTrue 
			$column.IsAuditEnabled.Value | Should -BeFalse
			$column.IsSecured | Should -BeFalse 
			$column.Targets | Should -Contain 'account'
			$column.Targets | Should -Contain 'contact'
		}

		It 'Add Polymorphic' {
			$output = Add-DataverseRelationship `
				-Table 'account' `
				-ColumnName "ams_testpolymorphic$($postfix)" `
				-ColumnDisplayName 'Test Polymorphic' `
				-ColumnRequired Recommended `
				-ColumnDescription 'Test Polymorphic Description' `
				-RelatedTable 'contact' `
				-Name "ams_testpolymorphic$($postfix)" `
				-Searchable `
				-Polymorphic

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.OneToManyRelationshipMetadata'
			$output.ReferencingEntity | Should -Be 'account'
			$output.ReferencedEntity | Should -Be 'contact'
			$output.SchemaName | Should -Be "ams_testpolymorphic$($postfix)"
			$output.IsValidForAdvancedFind | Should -BeTrue
			
			$column = Get-DataverseColumn -Table 'account' -Name "ams_testpolymorphic$($postfix)"
			$column | Should -BeOfType 'Microsoft.Xrm.Sdk.Metadata.LookupAttributeMetadata'
			$column.LogicalName | Should -Be "ams_testpolymorphic$($postfix)"
			$column.DisplayName.ToString() | Should -Be 'Test Polymorphic'
			$column.Description.ToString() | Should -Be 'Test Polymorphic Description'
			$column.RequiredLevel.Value | Should -Be Recommended
			$column.IsValidForAdvancedFind.Value | Should -BeTrue 
			$column.IsAuditEnabled.Value | Should -BeFalse
			$column.IsSecured | Should -BeFalse 
			$column.Targets | Should -Be @('contact')
		}
	}

	Context 'Remove-DataverseChoice' {

	}

	Context 'Remove-DataverseChoiceOption' {
		
	}

	Context 'Remove-DataverseColumn' {
		
	}

	Context 'Remove-DataverseRelationship' {
		
	}

	Context 'Remove-DataverseTableKey' {
		
	}

	Context 'Remove-DataverseTable' {
		
	}
}
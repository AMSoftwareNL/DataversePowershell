BeforeDiscovery {
	$TestDirectory = Split-Path $PSCommandPath -Parent
	$RootDirectory = Split-Path $TestDirectory -Parent
	$ModuleName = (Split-Path $PSCommandPath -Leaf) -Replace ".Tests.ps1"

	$ModuleDirectory = Resolve-Path -Path (Join-Path -Path $RootDirectory -ChildPath ".\dist\$($ModuleName)")
	$ManifestPath   =  Join-Path -Path $ModuleDirectory -ChildPath "$ModuleName.psd1"
}

BeforeAll {
	$TestDirectory = Split-Path $PSCommandPath -Parent
	$RootDirectory = Split-Path $TestDirectory -Parent
	$ModuleName = (Split-Path $PSCommandPath -Leaf) -Replace ".Tests.ps1"

	$ModuleDirectory = Resolve-Path -Path (Join-Path -Path $RootDirectory -ChildPath ".\dist\$($ModuleName)")
	$ManifestPath   =  Join-Path -Path $ModuleDirectory -ChildPath "$ModuleName.psd1"
}

Describe "AMSoftware.Dataverse.PowerShell" {
	Context "Manifest" {

		It "has a valid manifest" {
			{ 
				$Script:Manifest = Test-ModuleManifest -Path $ManifestPath -ErrorAction Stop -WarningAction SilentlyContinue
			} | Should -Not -Throw
		}		
	}

	Context "ScriptAnalyzer" {
		BeforeDiscovery {
			Install-Module PSScriptAnalyzer

			$scriptAnalyzerRules = Get-ScriptAnalyzerRule
			$Rules = @()
			$scriptAnalyzerRules | Foreach-Object { 
				$Rules += @{
					"RuleName" = $_.RuleName; 
					"Severity" = $_.Severity;
				} 
			}		
		}

		It "Passes <RuleName>" -ForEach $Rules {
			Invoke-ScriptAnalyzer -Path $ModuleDirectory -IncludeRule $RuleName |
			Foreach-Object { "Problem in $($_.ScriptName) at line $($_.Line) with message: $($_.Message)" } |
			 Should -BeNullOrEmpty
		}
	}

	Context "External Help" {
		BeforeDiscovery {
			$m = Import-PowershellDataFile $ManifestPath

			$commands = $m.CmdletsToExport
			$functions = $m.FunctionsToExport
		}

		BeforeAll {
			Import-Module -Name $ManifestPath -Scope Local -Force
		}

		It 'Cmdlet <_> has external Help' -ForEach $commands {
			Get-Command -Name $_ | Select-Object -ExpandProperty 'HelpFile' | Should -BeLike 'AMSoftware.Dataverse.PowerShell.*-Help.xml'
		}

		It 'Cmdlet <_> has online Help' -ForEach $commands {
			$h = Get-Help -Name $_
			$link = $h.relatedLinks.navigationLink[0].uri

			$link | Should -Not -BeNullOrEmpty
			$link | Should -Be "https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/$_.md"
		}

		It 'Function <_> has external Help' -ForEach $functions {
			Get-Command -Name $_ | Select-Object -ExpandProperty 'HelpFile' | Should -BeLike 'AMSoftware.Dataverse.PowerShell.*-Help.xml'
		}

		It 'Function <_> has online Help' -ForEach $functions {
			$h = Get-Help -Name $_
			$link = $h.relatedLinks.navigationLink[0].uri

			$link | Should -Not -BeNullOrEmpty
			$link | Should -Be "https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/$_.md"
		}
	}
}


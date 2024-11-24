BeforeAll {
	$TestDirectory = Split-Path $PSCommandPath -Parent
	$RootDirectory = Split-Path $TestDirectory -Parent
	$ModuleName = (Split-Path $PSCommandPath -Leaf) -Replace ".Tests.ps1"

	$ModuleDirectory = Resolve-Path -Path (Join-Path -Path $RootDirectory -ChildPath ".\dist\$($ModuleName)")
	$ManifestPath   = "$ModuleDirectory\$ModuleName.psd1"
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
}


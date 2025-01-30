pipeline {
	agent any
	
	stages{
		stage('Checkout'){
			steps{
				git branch: 'main', credentialsId: 'github-credentials', url: 'https://github.com/aathawerani/Utilitites.git'
			}
		}
		stage('Build'){
			steps{
				dir('GenerateQR\GenerateQR_v3\GenerateQR'){
					bat 'dotnet restore'
					bat 'dotnet build --configuration Release'
				}
			}
		}
	}
}
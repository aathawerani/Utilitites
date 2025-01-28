pipeline {
	agent any
	
	staes{
		stage('Checkout'){
			steps{
				git branch: 'main', credentialsId: 'github-credentials', url: 'https://github.com/aathawerani/Utilities'
			}
		}
		stage('Build'){
			steps{
				bat 'dotnet restore'
				bat 'dotnet build --configuration Release'
			}
		}
	}
}
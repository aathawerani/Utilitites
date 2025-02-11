pipeline {
	agent any
	
	environment {
        SONARQUBE_URL = 'http://localhost:9000'
        SONARQUBE_TOKEN = credentials('sonarqube-token') // Replace with your credential ID
    }
	
	stages{
		stage('Checkout'){
			steps{
				git branch: 'main', credentialsId: 'github-credentials', url: 'https://github.com/aathawerani/Utilitites.git'
			}
		}
		stage('Build'){
			steps{
				dir('GenerateQR/GenerateQR_v3/GenerateQR'){
					bat 'dotnet restore'
					bat 'dotnet build --configuration Release'
				}
			}
		}
		stage('SonarQube Analysis') {
            steps {
                withSonarQubeEnv('SonarQube') {
                    bat 'dotnet sonarscanner begin /k:"QR-code" /d:sonar.host.url=%SONARQUBE_URL% /d:sonar.login=%SONARQUBE_TOKEN%'
                    bat 'dotnet build --configuration Release'
                    bat 'dotnet sonarscanner end /d:sonar.login=%SONARQUBE_TOKEN%'
                }
            }
        }
	}
}
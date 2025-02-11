pipeline {
	agent any
	
	environment {
        SONARQUBE_URL = 'http://localhost:9000'
        SONARQUBE_TOKEN = credentials('c5ce0640-9155-42e4-9756-b09c801bf2f1') // Replace with your credential ID
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
		stage('Dependency Check') {
            steps {
                dependencyCheck additionalArguments: '--format HTML', outdir: 'dependency-check-report'
            }
        }
		stage('SonarQube Analysis') {
            steps {
                withSonarQubeEnv('SonarQube') {
					dir('GenerateQR/GenerateQR_v3/GenerateQR'){
						bat 'dotnet sonarscanner begin /k:"QR-code" /d:sonar.host.url=%SONARQUBE_URL% /d:sonar.login=%SONARQUBE_TOKEN%'
						bat 'dotnet build --configuration Release'
						bat 'dotnet sonarscanner end /d:sonar.login=%SONARQUBE_TOKEN%'
					}
				}
            }
        }
		stage('Run Tests') {
			steps {
				dir('GenerateQR/GenerateQR_v3/GenerateQR/GenerateQR.Tests') {
					bat 'dotnet test --logger trx --results-directory TestResults'
				}
			}
		}
	}
	post {
        always {
            archiveArtifacts artifacts: 'dependency-check-report/*.html', allowEmptyArchive: true
        }
    }
}
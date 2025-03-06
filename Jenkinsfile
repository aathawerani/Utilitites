pipeline {
	agent any
	
	environment {
        SONARQUBE_URL = 'http://localhost:9000'
        SONARQUBE_TOKEN = credentials('c5ce0640-9155-42e4-9756-b09c801bf2f1') // Replace with your credential ID
        DOCKER_IMAGE = "my-dotnet8-app"
        DOCKER_TAG = "latest"
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
				bat '"D:\\DevOps\\Dependency-Check\\bin\\dependency-check.bat" --project "QR-code" --scan . --format HTML --out dependency-check-report'
				dependencyCheckPublisher pattern: '**/dependency-check-report/dependency-check-report.html', failedTotalHigh: 1
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
		stage('Docker Build & Push') {
			steps {
				dir('GenerateQR/GenerateQR_v3/GenerateQR'){
					script {
						def imageName = "my-dotnet8-app"
						def imageTag = "latest"

						// Build Docker Image
						bat "docker build -t ${imageName}:${imageTag} ."

						// (Optional) Push to Docker Hub or Private Registry
						// bat "docker login -u <username> -p <password>"
						// bat "docker push ${imageName}:${imageTag}"
					}
				}
			}
		}
		stage('Deploy to Kubernetes using Ansible') {
            steps {
                script {
                    bat "ansible-playbook -i localhost, deploy_app.yaml"
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
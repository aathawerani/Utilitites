pipeline {
	agent any
	environment {
        SONARQUBE_URL = 'http://localhost:9000'
        SONARQUBE_TOKEN = credentials('c5ce0640-9155-42e4-9756-b09c801bf2f1') // Replace with your credential ID
        GIT_CREDENTIALS_ID = 'github-credentials'
        GITHUB_REPO = 'aathawerani/Utilitites'
        GITHUB_TOKEN = credentials('github-token')
        EMAIL_RECIPIENT = 'athawerani@gmail.com'
    }
	stages{
		stage('Checkout'){
			steps{
				git branch: 'main', credentialsId: 'github-credentials', url: 'https://github.com/aathawerani/Utilitites.git'
			}
		}
		stage('Dependency Check') {
            steps {
                bat '"D:\\DevOps\\Dependency-Check\\bin\\dependency-check.bat" --project "QR-code" --scan . --format JSON --format HTML --format XML --out dependency-check-report --nvdApiKey da276fc5-0eba-4a30-88ec-220c690c9d53 --log dependency-check.log'
                dependencyCheckPublisher(
				    pattern: '**/dependency-check-report/dependency-check-report.json',
				    failedTotalCritical: 1//,  // Pipeline fails if at least 1 Critical issue exists
				    //failedTotalHigh: 3,      // Pipeline fails if 3+ High issues exist
				    //failedTotalMedium: 5     // Pipeline fails if 5+ Medium issues exist
				)
            }
        }
		stage('Process Dependency-Check Results') {
            steps {
                script {
                    def reportFile = 'dependency-check-report/dependency-check-report.json'
                    def allIssues = []
                    def criticalIssues = [] 

                    if (!fileExists(reportFile)) {
		                error "Dependency-Check JSON report not found. Failing pipeline."
		            }

		            def jsonText = readFile(reportFile).trim()
		            if (!jsonText || jsonText == "{}") {
		                error "Dependency-Check JSON report is empty. Failing pipeline."
		            }

                    // Parse JSON report
                    def jsonReport = readJSON file: reportFile

                    def getExistingIssues = { ->
                        def response = bat(
                            script: """
                                "D:\\DevOps\\curl\\bin\\curl.exe" -s -X GET -H "Authorization: token %GITHUB_TOKEN%" ^
                                     -H "Accept: application/vnd.github.v3+json" ^
                                     https://api.github.com/repos/${GITHUB_REPO}/issues?state=open
                            """,
                            returnStdout: true
                        ).trim()

						response = response.substring(response.indexOf("["))
                        def issues = readJSON text: response
                        return issues
                    }

                    for (dep in jsonReport.dependencies) {
                        for (vuln in dep.vulnerabilities) {
                            def issueTitle = "[${vuln.severity}] ${vuln.name}"
                            def issueBody = "${vuln.name}: ${vuln.description}"
                            allIssues.add([title: issueTitle, body: issueBody])
                            if (vuln.severity?.trim().toLowerCase() == "critical") {
		                        criticalIssues.add(issueTitle + ": " + issueBody)
		                    }
                        }
                    }

                    // ✅ Call the closure like a function: doesIssueExist(issue.title)
                    def existingIssues = getExistingIssues()
					for (issue in allIssues) {
					    if (!existingIssues.any { it.title == issue.title }) {
					        withCredentials([string(credentialsId: 'github-token', variable: 'GITHUB_TOKEN')]) {
					            // ✅ Write JSON payload to a file
					            def jsonPayload = """
					            {
					                "title": "${issue.title}",
					                "body": "${issue.body.replace('"', '\\"')}",
					                "labels": ["security"]
					            }
					            """
					            writeFile file: "payload.json", text: jsonPayload

					            // ✅ Use `-d @payload.json` instead of inline JSON
					            bat """
					                "D:\\DevOps\\curl\\bin\\curl.exe" -X POST -H "Authorization: token %GITHUB_TOKEN%" ^
					                -H "Accept: application/vnd.github.v3+json" ^
					                https://api.github.com/repos/${GITHUB_REPO}/issues ^
					                -d @payload.json
					            """
					        }
					    } else {
					        echo "Issue '${issue.title}' already exists in GitHub. Skipping creation."
					    }
					}
					def htmlReport = 'dependency-check-report/dependency-check-report.html'
					if (!fileExists(htmlReport)) {
		                error "Dependency-Check HTML report not found. Skipping email."
		            }

		            def htmlContent = readFile(htmlReport)

					mail(
				        to: "${EMAIL_RECIPIENT}",
				        subject: "Dependency-Check Report",
				        body: """<html>
			                <body>
			                <p>Attached below is the security report.</p>
			                ${htmlContent}
			                </body></html>""",
			                mimeType: 'text/html'
				    )
				    echo "✅ Email sent with embedded HTML Dependency-Check report."
		            if (!criticalIssues.isEmpty()) {
					    //error "Pipeline halted due to ${criticalIssues.size()} critical security vulnerabilities."
					    echo "Critical issues found. Pipeline continuing for testing."
					} else {
		                echo "No critical issues found. Pipeline continuing."
		            }
                }
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
		            dir('GenerateQR/GenerateQR_v3/GenerateQR') {
		                bat '"C:\\Users\\ali.thawerani\\.dotnet\\tools\\dotnet-sonarscanner" begin /k:"QR-code" /d:sonar.host.url="http://localhost:9000" /d:sonar.login=%SONARQUBE_TOKEN%'
		                bat 'dotnet build --configuration Release'
		                bat '"C:\\Users\\ali.thawerani\\.dotnet\\tools\\dotnet-sonarscanner" end /d:sonar.login=%SONARQUBE_TOKEN%'
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
		stage('Create Pull Request to Deployment') {
		    steps {
		        script {
		            def GITHUB_TOKEN = credentials('github-token')  // GitHub Token stored in Jenkins credentials
		            def GITHUB_USERNAME = "aathawerani"  // Replace with your GitHub username
		            def GITHUB_EMAIL = "athawerani@gmail.com"
		            def REPO = "aathawerani/Utilitites"  // Replace with your repo name
		            def SOURCE_BRANCH = "main"
		            def TARGET_BRANCH = "deployment"
		            def PR_TITLE = "Automated PR: Merge ${SOURCE_BRANCH} into ${TARGET_BRANCH}"
		            def PR_BODY = "This PR was automatically generated by Jenkins."

		            // Set the correct Git user in Jenkins
		            bat "git config --global user.name \"${GITHUB_USERNAME}\""
		            bat "git config --global user.email \"${GITHUB_EMAIL}\""

		            // Create a pull request using GitHub API
					withCredentials([string(credentialsId: 'github-token', variable: 'GITHUB_TOKEN')]) {
					    bat """
					        "D:\\DevOps\\curl\\bin\\curl.exe" -X POST ^
					             -H "Authorization: token %GITHUB_TOKEN%" ^
					             -H "Accept: application/vnd.github.v3+json" ^
					             https://api.github.com/repos/aathawerani/Utilitites/pulls ^
					             -d "{\\"title\\": \\"Automated PR: Merge main into deployment\\", \\"body\\": \\"This PR was automatically generated by Jenkins.\\", \\"head\\": \\"main\\", \\"base\\": \\"deployment\\"}"
					    """
					}
		        }
		    }
		}
	}
	post {
        failure {
            script {
                def failedStage = ''
                def logs = ''

                // ✅ Detect the failed stage
                for (stage in currentBuild.rawBuild.getActions(hudson.model.ErrorAction)) {
                    failedStage = stage.displayName
                    break // Stop after the first failed stage
                }

                // ✅ Get the last 50 lines of Jenkins logs
                logs = currentBuild.rawBuild.getLog(50).join('\n')

                echo "🚨 Pipeline failed in stage: ${failedStage}"

                mail(
                    to: "${EMAIL_RECIPIENT}",
                    subject: "🚨 Jenkins Pipeline Failed in Stage: ${failedStage}",
                    body: """<html>
                    <body>
                    <h2>⚠️ Jenkins Pipeline Failure Notification</h2>
                    <p><b>Failed Stage:</b> ${failedStage}</p>
                    <p><b>Error Logs:</b></p>
                    <pre>${logs}</pre>
                    <p>Please check the Jenkins logs for more details.</p>
                    </body></html>""",
                    mimeType: 'text/html'
                )
            }
        }
    }
}
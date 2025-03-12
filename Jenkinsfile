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
                bat '"D:\\DevOps\\Dependency-Check\\bin\\dependency-check.bat" --project "QR-code" --scan . --format JSON --out dependency-check-report --nvdApiKey da276fc5-0eba-4a30-88ec-220c690c9d53 --log dependency-check.log'
            }
        }
		stage('Process Dependency-Check Results') {
		    steps {
		        script {
		            echo "Parsing file."
		            def reportFile = 'dependency-check-report/dependency-check-report.json'
		            def allIssues = []
		            def criticalIssues = []  // ✅ Declare it outside script block

		            // ✅ Check if JSON file exists before parsing
		            if (!fileExists(reportFile)) {
		                error "Dependency-Check JSON report not found. Failing pipeline."
		            }

		            def jsonText = readFile(reportFile).trim()
		            if (!jsonText || jsonText == "{}") {
		                error "Dependency-Check JSON report is empty. Failing pipeline."
		            }

		            // ✅ Parse JSON report
		            def jsonReport = readJSON text: jsonText
		            echo "File parsed."

		            // ✅ Extract issues from Dependency-Check report
		            for (dep in jsonReport.dependencies) {
		                for (vuln in dep.vulnerabilities) {
		                    def issueTitle = "[${vuln.severity}] ${vuln.name}"
		                    def issueBody = "${vuln.name}: ${vuln.description}"
		                    allIssues.add([title: issueTitle, body: issueBody])

		                    if (vuln.severity == "Critical") {
		                        criticalIssues.add(issueTitle + ": " + issueBody)
		                    }
		                }
		            }

		            // ✅ Fetch existing issues once to avoid redundant API calls
		            def existingIssues = getExistingIssues()

		            // ✅ Iterate over detected issues and create only new ones
		            for (issue in allIssues) {
		                if (!existingIssues.any { it.title == issue.title }) {
		                    withCredentials([string(credentialsId: 'github-token', variable: 'GITHUB_TOKEN')]) {
		                        def jsonPayload = """
		                        {
		                            "title": "${issue.title}",
		                            "body": "${issue.body.replace('"', '\\"')}",
		                            "labels": ["security"]
		                        }
		                        """
		                        writeFile file: "payload.json", text: jsonPayload

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

			        // ✅ Move emailext outside script block
			        emailext (
			            to: "${EMAIL_RECIPIENT}",
			            subject: "📊 Dependency-Check Report: Security Analysis",
			            body: "Attached is the full Dependency-Check security report.\n\nCritical Issues: ${criticalIssues.size()}\n\nPipeline execution: ${criticalIssues.size() > 0 ? 'HALTED 🚨' : 'CONTINUING ✅'}",
			            attachmentsPattern: "dependency-check-report/dependency-check-report.json"
			        )
			        echo "Email sent with full dependency-check report."

			        // ✅ Halt Pipeline if Critical Issues Exist
			        if (criticalIssues.size() > 0) {
			            error "Pipeline halted due to critical security vulnerabilities."
			        } else {
			            echo "No critical issues found. Pipeline continuing."
			        }
		        } // ✅ Close script block before emailext
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
}
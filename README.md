# Book Lending Solution

This is a .NET Core Web API project that manages book lending services.

Main idea of this project is to Add, checkout and return the books.

Please follow the below steps to run the application locally.

-> Navigate to https://github.com/priyanandavaram/BookLendingSolution and clone the repository on your local machine.

-> Set BookLendingSolution as the startup project and run the application and you'll displayed with swagger page (https://localhost:7245/swagger/index.html)

-> Test the endpoints using swagger and below is the sample Json data for performing HTTP operations.

# POST /api/books 
	
Sample Json Input
	
	{
		"Id":0,
		"BookTitle":"The Death Story",
		"BookAuthor":"Georgia Watson",
		"CheckedOutUser":""
	}

Sample Json Output after performing POST operation

	{
	"message": "Book has been added successfully.",
	"book": {
	  "id": 1,
	  "bookTitle": "The Death Story",
	  "bookAuthor": "Georgia Watson",
	  "checkedOutUser": "",
	  "checkedOutTime": "0001-01-01T00:00:00",
	  "isBookAvailable": true
		}
	}

# GET /api/books

Sample Json Output after performing GET operation
	
	{
		"id": 1,
		"bookTitle": "The Death Story",
		"bookAuthor": "Georgia Watson",
		"checkedOutUser": "",
		"checkedOutTime": "0001-01-01T00:00:00",
		"isBookAvailable": true
  }

# POST /api/books/checkout/1

Sample Json Input string
	
	"Priya"

Sample Json Output after checking out the book

	{
	  "message": "Book 'The Death Story' has been checked out by the user Priya."
	}

# POST /api/books/return/1

Sample Json Output after returning the book

	{
		"message": "Book 'The Death Story' has been returned successfully."
	}

Also, This solution is deployed to AWS Lambda + API Gateway services using the below process and we can test the endpoints using the below URL's.

1. https://github.com/priyanandavaram/BookLendingSolution, Navigate to the Settings -> Secrets and Variables -> Configured Repository Secrets for deployment.

2. In the BookLendingSolution, We've created a GitHub Actions workflow file (.github/workflows/pipeline.yml) to automate the deployment process to AWS Lambda + API Gateway services.

3. The GitHib workflow is triggered on every push to the master branch and it performs the following steps:

   - Builds the BookLendingSolution.slnx.
   - Run the tests.
   - Publishes the project and creates the BookLendingSolution.Zip file.
   - Configures AWS credentials using the secrets stored in the GitHub repository.
   - Uploads the BookLendingSolution.Zip file to the S3 bucket.
   - Runs the CloudFormation stack update (Template.yaml) to deploy the changes.
   - Updates the AWS Lambda function with the new deployment package from the S3 bucket.
   - Configures API Gateway to point to the updated Lambda function and generates the below API Gateway endpoints.

Below is the postman collection for testing the deployed API Gateway endpoints.

https://priyanandavaram-4461883.postman.co/workspace/Test's-Workspace~9c6ea062-f6c1-458b-8c2a-2b3f793baf12/collection/50695351-fcd5bfa0-9b2f-46dc-836d-921c3e6ca798?action=share&source=copy-link&creator=50695351

Or

Please refer to the Postman collection JSON file in the repository to import it into your Postman application



# API Documentation

Welcome to the API documentation. This guide provides all the information you need to integrate with our services. We've designed the API to be predictable, intuitive, and easy to use, with a focus on clear, actionable responses.

### Base URL

All API URLs referenced in this documentation have the following base:

`http://novaedapp-env.eba-ceaqmh3m.me-south-1.elasticbeanstalk.com`

### Authentication

Most endpoints in this API are protected and require a JSON Web Token (JWT) for authentication.

**Authentication Flow:**
1.  **Login:** Obtain an `accessToken` and a `refreshToken` by calling the **[User Login](#user-login)** (`/api/v1/users/login`) or **[External Provider Login](#external-provider-login)** (`/api/v1/users/external-login`) endpoints.
2.  **Authenticate Requests:** For every request to a protected endpoint, include the `accessToken` in the `Authorization` header.
3.  **Handle Expiration:** The `accessToken` has a limited lifespan. When it expires, the API will respond with a `401 Unauthorized` error.
4.  **Refresh Session:** When the `accessToken` expires, use the `refreshToken` with the **[Refresh Token](#refresh-token)** endpoint (`/api/v1/auth/refresh-token`) to get a new pair of tokens without forcing the user to log in again.

**Header Format:**
`Authorization: Bearer <YOUR_ACCESS_TOKEN>`

---

## Table of Contents
| Controller | Endpoint | Method | Path |
| :--- | :--- | :--- | :--- |
| [**Auth Controller**](AuthController.md) | | | |
| | [Refresh Token](AuthController.md#refresh-token) | `POST` | `/api/v1/auth/refresh-token` |
| [**Users Controller**](UsersController.md) | | | |
| | [User Signup](UsersController.md#user-signup) | `POST` | `/api/v1/users/signup` |
| | [User Login](UsersController.md#user-login) | `POST` | `/api/v1/users/login` |
| | [External Provider Login](UsersController.md#external-provider-login) | `POST` | `/api/v1/users/external-login` |
| | [Get Current User](UsersController.md#get-current-user) | `GET` | `/api/v1/users` |
| | [Update User](UsersController.md#update-user) | `PATCH` | `/api/v1/users` |
| | [User Logout](UsersController.md#user-logout) | `POST` | `/api/v1/users/logout` |
| [**Collections Controller**](CollectionsController.md) | | | |
| | [Create Collection](CollectionsController.md#create-collection) | `POST` | `/api/v1/collections` |
| | [Get All Collections](CollectionsController.md#get-all-collections) | `GET` | `/api/v1/collections` |
| | [Update Collection](CollectionsController.md#update-collection) | `PATCH` | `/api/v1/collections` |
| | [Delete Collection](CollectionsController.md#delete-collection) | `DELETE` | `/api/v1/collections` |
| | [Get Collection by ID](CollectionsController.md#get-collection-by-id) | `GET` | `/api/v1/collections/{CollecID}` |
| | [Search Collections](CollectionsController.md#search-collections) | `GET` | `/api/v1/collections/search` |
| | [Get Collections by User](CollectionsController.md#get-collections-by-user) | `GET` | `/api/v1/collections/users/{UserID}` |
| [**Collection Likes Controller**](CollectionLikesController.md) | | | |
| | [Like or Dislike a Collection](CollectionLikesController.md#like-or-dislike-a-collection) | `POST` | `/api/v1/collections/likes/Like` |
| [**Collection Reviews Controller**](CollectionReviewsController.md) | | | |
| | [Create a Review](CollectionReviewsController.md#create-a-review) | `POST` | `/api/v1/collections/reviews` |
| | [Update a Review](CollectionReviewsController.md#update-a-review) | `PATCH` | `/api/v1/collections/reviews` |
| | [Delete a Review](CollectionReviewsController.md#delete-a-review) | `DELETE` | `/api/v1/collections/reviews` |
| | [Get Reviews for a Collection](CollectionReviewsController.md#get-reviews-for-a-collection) | `GET` | `/api/v1/collections/reviews/{CollectionID}` |
| [**Collection Submissions Controller**](CollectionSubmissionsController.md)| | | |
| | [Create a Submission](CollectionSubmissionsController.md#create-a-submission) | `POST` | `/api/v1/submitions` |
| | [Get Submission Details](CollectionSubmissionsController.md#get-submission-details) | `GET` | `/api/v1/submitions` |
| | [Delete a Submission](CollectionSubmissionsController.md#delete-a-submission) | `DELETE` | `/api/v1/submitions` |
| [**Questions Controller**](QuestionsController.md) | | | |
| | [Add Questions to Collection](QuestionsController.md#add-questions-to-collection) | `POST` | `/api/v1/questions` |
| | [Get Questions from Collection](QuestionsController.md#get-questions-from-collection) | `GET` | `/api/v1/questions` |
| | [Delete a Question](QuestionsController.md#delete-a-question) | `DELETE` | `/api/v1/questions` |
| | [Get a Single Question](QuestionsController.md#get-a-single-question) | `GET` | `/api/v1/questions/{QuestionID}` |
| | [Update a Question](QuestionsController.md#update-a-question) | `PATCH` | `/api/v1/questions/{QuestionID}` |
| | [Update Question Points](QuestionsController.md#update-question-points) | `PATCH` | `/api/v1/questions/points/{QuestionID}` |
| | [Get Random Questions](QuestionsController.md#get-random-questions) | `GET` | `/api/v1/questions/random` |
| | [Get Right Answer and Explanation for a Question](QuestionsController.md#get-right-answer-and-explanation-for-a-question) | `GET` | `/api/v1/questions/right-answer-with-explanation/{questionID}` |
| [**Choices Controller**](ChoicesController.md) | | | |
| | [Add Choices to Question](ChoicesController.md#add-choices-to-question) | `POST` | `/api/v1/choices` |
| | [Get Choices by IDs](ChoicesController.md#get-choices-by-ids) | `GET` | `/api/v1/choices` |
| | [Get Choices for a Question](ChoicesController.md#get-choices-for-a-question) | `GET` | `/api/v1/choices/questions/{questionID}` |
| | [Get Right Answers for a Question](ChoicesController.md#get-right-answers-for-a-question) | `GET` | `/api/v1/choices/answers/{questionID}` |
| | [Get Choice with Explanation](ChoicesController.md#get-choice-with-explanation) | `GET` | `/api/v1/choices/explanation/{choiceId}/{questionId}` |
| | [Update a Choice](ChoicesController.md#update-a-choice) | `PATCH` | `/api/v1/choices/{ChoiceID}` |
| | [Delete a Choice](ChoicesController.md#delete-a-choice) | `DELETE` | `/api/v1/choices/{ChoiceID}` |
| [**Chosen Choices Controller**](ChosenChoicesController.md) | | | |
| | [Submit a Chosen Answer](ChosenChoicesController.md#submit-a-chosen-answer) | `POST` | `/api/v1/choices/chosen` |
| | [Get Submitted Answers](ChosenChoicesController.md#get-submitted-answers) | `GET` | `/api/v1/choices/chosen/submition/{submitionID}` |
| [**Explanations Controller**](ExplanationsController.md) | | | |
| | [Create an Explanation](ExplanationsController.md#create-an-explanation) | `POST` | `/api/v1/explanations` |
| | [Get Explanations for a Question](ExplanationsController.md#get-explanations-for-a-question) | `GET` | `/api/v1/explanations/questions/{QuestionID}` |
| | [Get a Single Explanation](ExplanationsController.md#get-a-single-explanation) | `GET` | `/api/v1/explanations/{ExplainID}` |
| [**Categories Controller**](CategoriesController.md) | | | |
| | [Create a Category](CategoriesController.md#create-a-category) | `POST` | `/api/v1/categories` |
| | [Get Categories](CategoriesController.md#get-categories) | `GET` | `/api/v1/categories/{RowsCount}` |
| [**Question Categories Controller**](QuestionCategoriesController.md)| | | |
| | [Get Categories for a Question](QuestionCategoriesController.md#get-categories-for-a-question) | `GET` | `/api/v1/categories/questions/{QuestionID}` |
| | [Add Categories to a Question](QuestionCategoriesController.md#add-categories-to-a-question) | `POST` | `/api/v1/categories/questions/{QuestionID}` |
| | [Delete All Categories from a Question](QuestionCategoriesController.md#delete-all-categories-from-a-question) | `DELETE` | `/api/v1/categories/questions/{QuestionID}` |
| | [Delete a Question-Category Link](QuestionCategoriesController.md#delete-a-question-category-link) | `DELETE` | `/api/v1/categories/{QuestionCategoryID}` |
| [**Home Screen Controller**](HomeScreenController.md) | | | |
| | [Get Top Collections](HomeScreenController.md#get-top-collections) | `GET` | `/api/v1/home/collections/top` |
| | [Get Top Followers](HomeScreenController.md#get-top-followers) | `GET` | `/api/v1/home/followers/top` |
| [**Health & Test Controller**](HealthAndTestController.md) | | | |
| | [Health Check](HealthAndTestController.md#health-check) | `GET` | `/api/health` |
| | [Get Secure Metrics](HealthAndTestController.md#get-secure-metrics) | `GET` | `/api/secure-metrics` |
| | [Get Claims (Test)](HealthAndTestController.md#get-claims-test) | `GET` | `/API/Test/GetClaims` |
| | [Create New Token (Test)](HealthAndTestController.md#create-new-token-test) | `POST` | `/API/Test/CreateNewToken` |
| | [Upload Image (Test)](HealthAndTestController.md#upload-image-test) | `POST` | `/API/Test/UploadImage` |
| | [Test Critical Log (Test)](HealthAndTestController.md#test-critical-log-test) | `GET` | `/API/Test/TestCriticalLog` |
| | [External Login (Test)](HealthAndTestController.md#external-login-test) | `POST` | `/API/Test/external-login` |


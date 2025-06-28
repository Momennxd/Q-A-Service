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
| | [User Signup](#user-signup) | `POST` | `/api/v1/users/signup` |
| | [User Login](#user-login) | `POST` | `/api/v1/users/login` |
| | [External Provider Login](#external-provider-login) | `POST` | `/api/v1/users/external-login` |
| | [Get Current User](#get-current-user) | `GET` | `/api/v1/users` |
| | [Update User](#update-user) | `PATCH` | `/api/v1/users` |
| | [User Logout](#user-logout) | `POST` | `/api/v1/users/logout` |
| [**Collections Controller**]() | | | |
| | [Create Collection](#create-collection) | `POST` | `/api/v1/collections` |
| | [Get All Collections](#get-all-collections) | `GET` | `/api/v1/collections` |
| | [Update Collection](#update-collection) | `PATCH` | `/api/v1/collections` |
| | [Delete Collection](#delete-collection) | `DELETE` | `/api/v1/collections` |
| | [Get Collection by ID](#get-collection-by-id) | `GET` | `/api/v1/collections/{CollecID}` |
| | [Search Collections](#search-collections) | `GET` | `/api/v1/collections/search` |
| | [Get Collections by User](#get-collections-by-user) | `GET` | `/api/v1/collections/users/{UserID}` |
| **Collection Likes Controller** | | | |
| | [Like or Dislike a Collection](#like-or-dislike-a-collection) | `POST` | `/api/v1/collections/likes/Like` |
| **Collection Reviews Controller** | | | |
| | [Create a Review](#create-a-review) | `POST` | `/api/v1/collections/reviews` |
| | [Update a Review](#update-a-review) | `PATCH` | `/api/v1/collections/reviews` |
| | [Delete a Review](#delete-a-review) | `DELETE` | `/api/v1/collections/reviews` |
| | [Get Reviews for a Collection](#get-reviews-for-a-collection) | `GET` | `/api/v1/collections/reviews/{CollectionID}` |
| **Collection Submissions Controller**| | | |
| | [Create a Submission](#create-a-submission) | `POST` | `/api/v1/submitions` |
| | [Get Submission Details](#get-submission-details) | `GET` | `/api/v1/submitions` |
| | [Delete a Submission](#delete-a-submission) | `DELETE` | `/api/v1/submitions` |
| **Questions Controller** | | | |
| | [Add Questions to Collection](#add-questions-to-collection) | `POST` | `/api/v1/questions` |
| | [Get Questions from Collection](#get-questions-from-collection) | `GET` | `/api/v1/questions` |
| | [Delete a Question](#delete-a-question) | `DELETE` | `/api/v1/questions` |
| | [Get a Single Question](#get-a-single-question) | `GET` | `/api/v1/questions/{QuestionID}` |
| | [Update a Question](#update-a-question) | `PATCH` | `/api/v1/questions/{QuestionID}` |
| | [Update Question Points](#update-question-points) | `PATCH` | `/api/v1/questions/points/{QuestionID}` |
| | [Get Random Questions](#get-random-questions) | `GET` | `/api/v1/questions/random` |
| **Choices Controller** | | | |
| | [Add Choices to Question](#add-choices-to-question) | `POST` | `/api/v1/choices` |
| | [Get Choices by IDs](#get-choices-by-ids) | `GET` | `/api/v1/choices` |
| | [Get Choices for a Question](#get-choices-for-a-question) | `GET` | `/api/v1/choices/questions/{questionID}` |
| | [Get Right Answers for a Question](#get-right-answers-for-a-question) | `GET` | `/api/v1/choices/answers/{questionID}` |
| | [Get Choice with Explanation](#get-choice-with-explanation) | `GET` | `/api/v1/choices/explanation/{choiceId}/{questionId}` |
| | [Update a Choice](#update-a-choice) | `PATCH` | `/api/v1/choices/{ChoiceID}` |
| | [Delete a Choice](#delete-a-choice) | `DELETE` | `/api/v1/choices/{ChoiceID}` |
| **Chosen Choices Controller** | | | |
| | [Submit a Chosen Answer](#submit-a-chosen-answer) | `POST` | `/api/v1/choices/chosen` |
| | [Get Submitted Answers](#get-submitted-answers) | `GET` | `/api/v1/choices/chosen/submition/{submitionID}` |
| **Explanations Controller** | | | |
| | [Create an Explanation](#create-an-explanation) | `POST` | `/api/v1/explanations` |
| | [Get Explanations for a Question](#get-explanations-for-a-question) | `GET` | `/api/v1/explanations/questions/{QuestionID}` |
| | [Get a Single Explanation](#get-a-single-explanation) | `GET` | `/api/v1/explanations/{ExplainID}` |
| **Categories Controller** | | | |
| | [Create a Category](#create-a-category) | `POST` | `/api/v1/categories` |
| | [Get Categories](#get-categories) | `GET` | `/api/v1/categories/{RowsCount}` |
| **Question Categories Controller**| | | |
| | [Get Categories for a Question](#get-categories-for-a-question) | `GET` | `/api/v1/categories/questions/{QuestionID}` |
| | [Add Categories to a Question](#add-categories-to-a-question) | `POST` | `/api/v1/categories/questions/{QuestionID}` |
| | [Delete All Categories from a Question](#delete-all-categories-from-a-question) | `DELETE` | `/api/v1/categories/questions/{QuestionID}` |
| | [Delete a Question-Category Link](#delete-a-question-category-link) | `DELETE` | `/api/v1/categories/{QuestionCategoryID}` |
| **Home Screen Controller** | | | |
| | [Get Top Collections](#get-top-collections) | `GET` | `/api/v1/home/collections/top` |
| | [Get Top Followers](#get-top-followers) | `GET` | `/api/v1/home/followers/top` |
| [**Health & Test Controller**](HealthAndTestController.md) | | | |
| | [Health Check](#health-check) | `GET` | `/api/health` |
| | [Get Secure Metrics](#get-secure-metrics) | `GET` | `/api/secure-metrics` |
| | [Get Claims (Test)](#get-claims-test) | `GET` | `/API/Test/GetClaims` |
| | [Create New Token (Test)](#create-new-token-test) | `POST` | `/API/Test/CreateNewToken` |
| | [Upload Image (Test)](#upload-image-test) | `POST` | `/API/Test/UploadImage` |
| | [Test Critical Log (Test)](#test-critical-log-test) | `GET` | `/API/Test/TestCriticalLog` |
| | [External Login (Test)](#external-login-test) | `POST` | `/API/Test/external-login` |


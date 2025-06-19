# Q-A Service API Documentation

This document provides detailed information about the API endpoints for the Q-A Service.

**GitHub Repository:** [https://github.com/Momennxd/Q-A-Service](https://github.com/Momennxd/Q-A-Service)

## Table of Contents
| Controller | Endpoint | Method | Path |
| :--- | :--- | :--- | :--- |
| **Auth Controller** | | | |
| | [Refresh Access Token](#refresh-access-token) | `POST` | `/api/auth/refresh-token` |
| **Users Controller** | | | |
| | [User Signup](#user-signup) | `POST` | `/api/v1/users/signup` |
| | [User Login](#user-login) | `POST` | `/api/v1/users/login` |
| | [External Provider Login](#external-provider-login) | `POST` | `/api/v1/users/external-login` |
| | [Get Current User](#get-current-user) | `GET` | `/api/v1/users` |
| | [Update User](#update-user) | `PATCH` | `/api/v1/users` |
| | [User Logout](#user-logout) | `POST` | `/api/v1/users/logout` |
| **Collections Controller** | | | |
| | [Create Collection](#create-collection) | `POST` | `/api/v1/collections` |
| | [Get All Collections (by user)](#get-all-collections-by-user) | `GET` | `/api/v1/collections` |
| | [Get Collections by User ID](#get-collections-by-user-id) | `GET` | `/api/v1/collections/users/{UserID}` |
| | [Get Collection by ID](#get-collection-by-id) | `GET` | `/api/v1/collections/{CollecID}` |
| | [Search Collections](#search-collections) | `GET` | `/api/v1/collections/search` |
| | [Update Collection](#update-collection) | `PATCH` | `/api/v1/collections` |
| | [Delete Collection](#delete-collection) | `DELETE` | `/api/v1/collections` |
| | [Like/Unlike a Collection](#likeunlike-a-collection) | `POST` | `/api/v1/collections/likes/Like` |
| | [Add a Review](#add-a-review) | `POST` | `/api/v1/collections/reviews` |
| | [Get Reviews for a Collection](#get-reviews-for-a-collection) | `GET` | `/api/v1/collections/reviews/{CollectionID}` |
| | [Start a Collection Submission (Quiz)](#start-a-collection-submission-quiz) | `POST` | `/api/v1/submitions` |
| **Questions Controller** | | | |
| | [Add Questions to a Collection](#add-questions-to-a-collection) | `POST` | `/api/v1/questions` |
| | [Get Questions from a Collection](#get-questions-from-a-collection) | `GET` | `/api/v1/questions` |
| | [Get Random Questions](#get-random-questions) | `GET` | `/api/v1/questions/random` |
| | [Get a Single Question](#get-a-single-question) | `GET` | `/api/v1/questions/{QuestionID}` |
| | [Update a Question](#update-a-question) | `PATCH` | `/api/v1/questions/{QuestionID}` |
| | [Delete a Question](#delete-a-question) | `DELETE` | `/api/v1/questions` |
| **Choices Controller** | | | |
| | [Add Choices to a Question](#add-choices-to-a-question) | `POST` | `/api/v1/choices` |
| | [Get Choices for a Question](#get-choices-for-a-question) | `GET` | `/api/v1/choices/questions/{questionID}` |
| | [Get Right Answer and Explanation](#get-right-answer-and-explanation) | `GET` | `/api/v1/choices/explanation/{choiceId}/{questionId}` |
| | [Submit a Chosen Answer](#submit-a-chosen-answer) | `POST` | `/api/v1/choices/chosen` |
| | [Get Submitted Answers for a Submission](#get-submitted-answers-for-a-submission) | `GET` | `/api/v1/choices/chosen/submition/{submitionID}` |
| | [Update a Choice](#update-a-choice) | `PATCH` | `/api/v1/choices/{ChoiceID}` |
| | [Delete a Choice](#delete-a-choice) | `DELETE` | `/api/v1/choices/{ChoiceID}` |
| **Home Screen Controller** | | | |
| | [Get Top Collections](#get-top-collections) | `GET` | `/api/v1/home/collections/top` |
| | [Get Top Followers](#get-top-followers) | `GET` | `/api/v1/home/followers/top` |

## Base URL

The base URL for all API endpoints is: `http://novaedapp-env.eba-ceaqmh3m.me-south-1.elasticbeanstalk.com`


## Authentication

Most endpoints in this API are protected and require authentication. The authentication process follows the standard JWT (JSON Web Token) flow:

1.  **Login:** A user logs in using the `POST /api/v1/users/login` (for standard credentials) or `POST /api/v1/users/external-login` (for social logins) endpoint.
2.  **Receive Tokens:** Upon successful login, the API returns an `accessToken` and a `refreshToken`.
3.  **Authorize Requests:** For all subsequent requests to protected endpoints, you must include the `accessToken` in the `Authorization` header as a Bearer token.
    - **Header:** `Authorization: Bearer <your_accessToken>`
4.  **Refresh Token:** The `accessToken` has a limited lifespan. When it expires, use the `refreshToken` with the `POST /api/auth/refresh-token` endpoint to obtain a new pair of tokens without requiring the user to log in again.

---

## API Endpoints

### Auth Controller

Endpoints related to token management.

#### Refresh Access Token
Refreshes an expired access token using a valid refresh token.

- **Method:** `POST`
- **Path:** `/api/auth/refresh-token`
- **Request Body:**
  ```json
  {
    "token": "string"
  }
  ```
- **Success Response (200 OK):**
  ```json
  {
    "accessToken": "string",
    "refreshToken": "string"
  }
  ```

---

### Users Controller

Endpoints for user registration, login, and profile management.

#### User Signup
Registers a new user in the system.

- **Method:** `POST`
- **Path:** `/api/v1/users/signup`
- **Request Body:**
  ```json
  {
    "username": "string",
    "password": "string",
    "person": {
      "firstName": "string",
      "secondName": "string",
      "lastName": "string",
      "address": "string",
      "gender": boolean,
      "countryID": integer,
      "dateOfBirth": "date-time",
      "email": "string",
      "notes": "string",
      "preferredLanguageID": integer
    }
  }
  ```
- **Success Response (200 OK):**
  ```json
  {
    "userId": integer,
    "username": "string",
    "person": {
      "firstName": "string",
      "secondName": "string",
      "lastName": "string",
      "address": "string",
      "gender": boolean,
      "countryID": integer,
      "dateOfBirth": "date-time",
      "email": "string",
      "notes": "string",
      "preferredLanguageID": integer,
      "personID": integer,
      "joinedDate": "date-time"
    }
  }
  ```

#### User Login
Authenticates a user with a username and password to receive access and refresh tokens.

- **Method:** `POST`
- **Path:** `/api/v1/users/login`
- **Request Body:**
  ```json
  {
    "username": "string",
    "password": "string"
  }
  ```
- **Success Response (200 OK):**
  ```json
  {
    "accessToken": "string",
    "refreshToken": "string"
  }
  ```

#### External Provider Login
Authenticates or signs up a user using an external provider token (e.g., Google, Facebook).

- **Method:** `POST`
- **Path:** `/api/v1/users/external-login`
- **Request Body:**
  ```json
  {
    "provider": "string",
    "idToken": "string"
  }
  ```
- **Success Response (200 OK):**
  ```json
  {
    "user": {
      "userId": integer,
      "username": "string",
      "firstName": "string",
      "secondName": "string",
      "lastName": "string",
      "gender": boolean,
      "email": "string",
      "notes": "string",
      "userPoints": integer
    },
    "tokens": {
      "accessToken": "string",
      "refreshToken": "string"
    }
  }
  ```

#### Get Current User
Retrieves the profile information for the currently authenticated user.

- **Method:** `GET`
- **Path:** `/api/v1/users`
- **Authentication:** Required.
- **Success Response (200 OK):**
  ```json
  {
    "userId": integer,
    "username": "string",
    "firstName": "string",
    "secondName": "string",
    "lastName": "string",
    "gender": boolean,
    "email": "string",
    "notes": "string",
    "userPoints": integer
  }
  ```

#### Update User
Updates details of the authenticated user using a JSON Patch request.

- **Method:** `PATCH`
- **Path:** `/api/v1/users`
- **Authentication:** Required.
- **Request Body:** An array of JSON Patch operations.
  ```json
  [
    {
      "op": "replace",
      "path": "/firstName",
      "value": "NewFirstName"
    }
  ]
  ```
- **Success Response (200 OK):** The updated `SendUserDTO` object (see User Signup response).

#### User Logout
Invalidates the user's refresh token, effectively logging them out.

- **Method:** `POST`
- **Path:** `/api/v1/users/logout`
- **Request Body:**
  ```json
  {
    "token": "string"
  }
  ```
- **Success Response (200 OK):** Success message.

---

### Collections Controller

Endpoints for creating, managing, and interacting with question collections.

#### Create Collection
Creates a new question collection.

- **Method:** `POST`
- **Path:** `/api/v1/collections`
- **Authentication:** Required.
- **Request Body:**
  ```json
  {
    "collectionName": "string",
    "description": "string",
    "isPublic": boolean,
    "collecQuestions": [
      {
        "questionText": "string",
        "isMCQ": boolean,
        "questionPoints": integer,
        "rank": integer,
        "choices": [
          {
            "choiceText": "string",
            "isRightAnswer": boolean,
            "rank": integer
          }
        ]
      }
    ]
  }
  ```
- **Success Response (200 OK):** Success message.

#### Get All Collections (by user)
Retrieves all collections created by the authenticated user.

- **Method:** `GET`
- **Path:** `/api/v1/collections`
- **Authentication:** Required.
- **Success Response (200 OK):** A list of collections.

#### Get Collections by User ID
Retrieves all public collections for a specific user.

- **Method:** `GET`
- **Path:** `/api/v1/collections/users/{UserID}`
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `UserID` | path | True | integer | The ID of the user. |
- **Success Response (200 OK):** A list of collections.

#### Get Collection by ID
Retrieves a specific collection by its ID.

- **Method:** `GET`
- **Path:** `/api/v1/collections/{CollecID}`
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `CollecID` | path | True | integer | The ID of the collection. |
- **Success Response (200 OK):** The collection object.

#### Search Collections
Searches for public collections based on text.

- **Method:** `GET`
- **Path:** `/api/v1/collections/search`
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `SearchText` | query | False | string | The text to search for. |
| `PageNumber` | query | False | integer | The page number for pagination. |
| `PageSize` | query | False | integer | The number of results per page. |
- **Success Response (200 OK):** A paginated list of collections.

#### Update Collection
Updates a collection's details using a JSON Patch request.

- **Method:** `PATCH`
- **Path:** `/api/v1/collections`
- **Authentication:** Required.
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `CollecID` | query | True | integer | The ID of the collection to update. |
- **Request Body:** An array of JSON Patch operations.
- **Success Response (200 OK):** Success message.

#### Delete Collection
Deletes a collection.

- **Method:** `DELETE`
- **Path:** `/api/v1/collections`
- **Authentication:** Required.
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `CollecID` | query | True | integer | The ID of the collection to delete. |
- **Success Response (200 OK):** Success message.

#### Like/Unlike a Collection
Adds or removes a like from a collection.

- **Method:** `POST`
- **Path:** `/api/v1/collections/likes/Like`
- **Authentication:** Required.
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `CollectionId` | query | True | integer | The ID of the collection. |
| `IsLike` | query | True | boolean | `true` to like, `false` to unlike. |
- **Success Response (200 OK):** Success message.

#### Add a Review
Adds a review to a collection.

- **Method:** `POST`
- **Path:** `/api/v1/collections/reviews`
- **Authentication:** Required.
- **Request Body:**
  ```json
  {
    "collectionID": integer,
    "userID": integer,
    "reviewText": "string",
    "reviewValue": integer,
    "reviewDate": "date-time"
  }
  ```
- **Success Response (200 OK):** Success message.

#### Get Reviews for a Collection
Retrieves all reviews for a specific collection.

- **Method:** `GET`
- **Path:** `/api/v1/collections/reviews/{CollectionID}`
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `CollectionID` | path | True | integer | The ID of the collection. |
| `Page` | query | False | integer | The page number for pagination. |
- **Success Response (200 OK):** A list of reviews.

#### Start a Collection Submission (Quiz)
Initializes a new submission (quiz session) for a collection and returns a unique submission ID.

- **Method:** `POST`
- **Path:** `/api/v1/submitions`
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `CollectionID`| query | True | integer | The ID of the collection to start. |
- **Success Response (200 OK):**
  ```json
  integer // The unique SubmitionID
  ```

---

### Questions Controller

Endpoints for managing questions within collections.

#### Add Questions to a Collection
Adds one or more questions to an existing collection.

- **Method:** `POST`
- **Path:** `/api/v1/questions`
- **Authentication:** Required.
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `CollectionID`| query | True | integer | The ID of the collection to add questions to. |
- **Request Body:** An array of `CreateQuestionDTO` objects.
  ```json
  [
    {
      "questionText": "What is 2+2?",
      "isMCQ": true,
      "questionPoints": 10,
      "rank": 1,
      "choices": [
        { "choiceText": "4", "isRightAnswer": true, "rank": 1 },
        { "choiceText": "3", "isRightAnswer": false, "rank": 2 }
      ]
    }
  ]
  ```
- **Success Response (200 OK):** Success message.

#### Get Questions from a Collection
Retrieves all questions for a given collection.

- **Method:** `GET`
- **Path:** `/api/v1/questions`
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `CollectionID`| query | True | integer | The ID of the collection. |
- **Success Response (200 OK):** A list of questions.

#### Get Random Questions
Retrieves a random set of questions from a collection, typically for a quiz.

- **Method:** `GET`
- **Path:** `/api/v1/questions/random`
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `collectionId`| query | True | integer | The ID of the collection. |
- **Success Response (200 OK):**
  ```json
  [
    {
      "questionID": integer,
      "questionText": "string",
      "rank": integer,
      "choices": [
        {
          "choiceID": integer,
          "choiceText": "string"
        }
      ]
    }
  ]
  ```

#### Get a Single Question
Retrieves a specific question by its ID.

- **Method:** `GET`
- **Path:** `/api/v1/questions/{QuestionID}`
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `QuestionID`| path | True | integer | The ID of the question. |
- **Success Response (200 OK):** The question object.

#### Update a Question
Updates a question's details using a JSON Patch request.

- **Method:** `PATCH`
- **Path:** `/api/v1/questions/{QuestionID}`
- **Authentication:** Required.
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `QuestionID`| path | True | integer | The ID of the question to update. |
- **Request Body:** An array of JSON Patch operations.
- **Success Response (200 OK):** Success message.

#### Delete a Question
Deletes a question from a collection.

- **Method:** `DELETE`
- **Path:** `/api/v1/questions`
- **Authentication:** Required.
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `QuestionID`| query | True | integer | The ID of the question to delete. |
- **Success Response (200 OK):** Success message.

---

### Choices Controller

Endpoints for managing choices for questions and handling user answers.

#### Add Choices to a Question
Adds one or more choices to an existing question.

- **Method:** `POST`
- **Path:** `/api/v1/choices`
- **Authentication:** Required.
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `QuestionID`| query | True | integer | The ID of the question. |
- **Request Body:** An array of `CreateChoiceDTO` objects.
  ```json
  [
    {
      "choiceText": "string",
      "isRightAnswer": boolean,
      "rank": integer
    }
  ]
  ```
- **Success Response (200 OK):** Success message.

#### Get Choices for a Question
Retrieves all choices associated with a specific question.

- **Method:** `GET`
- **Path:** `/api/v1/choices/questions/{questionID}`
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `questionID`| path | True | integer | The ID of the question. |
- **Success Response (200 OK):** A list of choices.

#### Get Right Answer and Explanation
Retrieves a specific choice along with its explanation text (if available). Used for showing results after an answer.

- **Method:** `GET`
- **Path:** `/api/v1/choices/explanation/{choiceId}/{questionId}`
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `choiceId` | path | True | integer | The ID of the chosen answer. |
| `questionId`| path | True | integer | The ID of the question. |
- **Success Response (200 OK):**
  ```json
  {
    "choiceID": integer,
    "choiceText": "string",
    "isRightAnswer": boolean,
    "explanationText": "string",
    "explanationID": integer
  }
  ```

#### Submit a Chosen Answer
Submits a user's choice for a question as part of a quiz submission.

- **Method:** `POST`
- **Path:** `/api/v1/choices/chosen`
- **Authentication:** Required.
- **Request Body:**
  ```json
  {
    "choiceID": integer,
    "submitionID": integer
  }
  ```
- **Success Response (200 OK):** Success message.

#### Get Submitted Answers for a Submission
Retrieves the choices selected by a user for a specific submission.

- **Method:** `GET`
- **Path:** `/api/v1/choices/chosen/submition/{submitionID}`
- **Authentication:** Required.
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `submitionID`| path | True | integer | The unique ID of the submission. |
| `QuestionIDs`| header| False | array[int] | Optional: Filter by specific question IDs. |
- **Success Response (200 OK):** A list of chosen choices.

#### Update a Choice
Updates a choice's details using a JSON Patch request.

- **Method:** `PATCH`
- **Path:** `/api/v1/choices/{ChoiceID}`
- **Authentication:** Required.
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `ChoiceID`| path | True | integer | The ID of the choice to update. |
- **Request Body:** An array of JSON Patch operations.
- **Success Response (200 OK):** Success message.

#### Delete a Choice
Deletes a choice.

- **Method:** `DELETE`
- **Path:** `/api/v1/choices/{ChoiceID}`
- **Authentication:** Required.
- **Parameters:**

| Name | In | Required | Type | Description |
|---|---|---|---|---|
| `ChoiceID`| path | True | integer | The ID of the choice to delete. |
- **Success Response (200 OK):** Success message.

---

### Home Screen Controller

Endpoints for retrieving curated content for the main application screen.

#### Get Top Collections
Retrieves a list of top-rated or most popular collections.

- **Method:** `GET`
- **Path:** `/api/v1/home/collections/top`
- **Success Response (200 OK):**
  ```json
  [
    {
      "collectionName": "string",
      "description": "string",
      "isPublic": boolean,
      "collectionID": integer,
      "addedTime": "date-time",
      "categories": [
        {
          "categoryName": "string"
        }
      ]
    }
  ]
  ```

#### Get Top Followers
Retrieves a list of the top followed users.

- **Method:** `GET`
- **Path:** `/api/v1/home/followers/top`
- **Success Response (200 OK):** Success. (Response body schema not defined in Swagger doc).

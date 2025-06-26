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
| **Auth Controller** | | | |
| | [Refresh Token](#refresh-token) | `POST` | `/api/v1/auth/refresh-token` |
| **Users Controller** | | | |
| | [User Signup](#user-signup) | `POST` | `/api/v1/users/signup` |
| | [User Login](#user-login) | `POST` | `/api/v1/users/login` |
| | [External Provider Login](#external-provider-login) | `POST` | `/api/v1/users/external-login` |
| | [Get Current User](#get-current-user) | `GET` | `/api/v1/users` |
| | [Update User](#update-user) | `PATCH` | `/api/v1/users` |
| | [User Logout](#user-logout) | `POST` | `/api/v1/users/logout` |
| **Collections Controller** | | | |
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
| **Health & Test Controller** | | | |
| | [Health Check](#health-check) | `GET` | `/api/health` |
| | [Get Secure Metrics](#get-secure-metrics) | `GET` | `/api/secure-metrics` |
| | [Get Claims (Test)](#get-claims-test) | `GET` | `/API/Test/GetClaims` |
| | [Create New Token (Test)](#create-new-token-test) | `POST` | `/API/Test/CreateNewToken` |
| | [Upload Image (Test)](#upload-image-test) | `POST` | `/API/Test/UploadImage` |
| | [Test Critical Log (Test)](#test-critical-log-test) | `GET` | `/API/Test/TestCriticalLog` |
| | [External Login (Test)](#external-login-test) | `POST` | `/API/Test/external-login` |

---
---

# Auth Controller
<a id="auth-controller"></a>

Endpoints for handling authentication-related tasks, specifically refreshing access tokens.

---
### Refresh Token
<a id="refresh-token"></a>

> `POST /api/v1/auth/refresh-token`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/auth/refresh-token` | **Not Required** (Token is in body) |

**Description**
When a user's `accessToken` expires, the client application should use this endpoint to get a new one without forcing the user to log in again. Provide the `refreshToken` obtained during the initial login. A successful call returns a new pair of access and refresh tokens.

**Request Body** (`RefreshTokenDTO`)
```json
{
  "token": "string"
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `TokenResponseDto` object. |
| `404 Not Found` | Invalid Token | The provided refresh token is invalid, expired, or has been revoked. The user must log in again. |

*Example `200 OK` Response (`TokenResponseDto`):*
```json
{
  "accessToken": "string",
  "refreshToken": "string"
}
```
---
---

# Users Controller
<a id="users-controller"></a>

Manages all aspects of user accounts, including registration, login, logout, and profile management.

---
### User Signup
<a id="user-signup"></a>

> `POST /api/v1/users/signup`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/users/signup` | Not Required |

**Description**
Creates a new user account with a username, password, and associated personal details. A successful registration returns the newly created user's profile information, confirming the account has been created.

**Request Body** (`AddUserDTO`)
```json
{
  "username": "string",
  "password": "string",
  "person": {
    "firstName": "string",
    "secondName": "string",
    "lastName": "string",
    "address": "string",
    "gender": true,
    "countryID": 0,
    "dateOfBirth": "2024-05-21T12:00:00Z",
    "email": "string",
    "notes": "string",
    "preferredLanguageID": 0
  }
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `SendUserDTO` object. |
| `400 Bad Request` | Validation Error | The request is invalid. This could be due to a username that already exists, a weak password, or other failed validation rules. |

*Example `200 OK` Response (`SendUserDTO`):*
```json
{
  "userId": 0,
  "username": "string",
  "person": {
    "firstName": "string",
    "secondName": "string",
    "lastName": "string",
    "address": "string",
    "gender": true,
    "countryID": 0,
    "dateOfBirth": "2024-05-21T12:00:00Z",
    "email": "string",
    "notes": "string",
    "preferredLanguageID": 0,
    "personID": 0,
    "joinedDate": "2024-05-21T12:00:00Z"
  }
}
```
---
### User Login
<a id="user-login"></a>

> `POST /api/v1/users/login`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/users/login` | Not Required |

**Description**
This is the primary endpoint for authenticating a user with their username and password. A successful login provides an `accessToken` (for API access) and a `refreshToken` (for session renewal).

**Request Body** (`LoginDTO`)
```json
{
  "username": "string",
  "password": "string"
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `LoginResponseDto` object. |
| `401 Unauthorized` | Invalid Credentials | The provided username or password was incorrect. |

*Example `200 OK` Response (`LoginResponseDto`):*
```json
{
  "accessToken": "string",
  "refreshToken": "string"
}
```
---
### External Provider Login
<a id="external-provider-login"></a>

> `POST /api/v1/users/external-login`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/users/external-login` | Not Required |

**Description**
Handles both login and registration for users authenticating with a third-party OAuth provider (e.g., Google, Facebook). Send the token provided by the external service. If a user with the associated external ID exists, they are logged in. If not, a new user account is created and then logged in.

**Request Body** (`ExternalLoginRequestDTO`)
```json
{
  "provider": "string",
  "idToken": "string"
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An `ExternalAuthResponseDTO` object. |
| `400 Bad Request` | Invalid Token | The provider token is invalid, expired, or could not be verified by the server. |

*Example `200 OK` Response (`ExternalAuthResponseDTO`):*
```json
{
  "user": {
    "userId": 0,
    "username": "string",
    "firstName": "string",
    "secondName": "string",
    "lastName": "string",
    "gender": true,
    "email": "string",
    "notes": "string",
    "userPoints": 0
  },
  "tokens": {
    "accessToken": "string",
    "refreshToken": "string"
  }
}
```
---
### Get Current User
<a id="get-current-user"></a>

> `GET /api/v1/users`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/users` | **Required** |

**Description**
Fetches the detailed profile of the user associated with the `accessToken` sent in the `Authorization` header. This is useful for populating a user profile page or checking current user details.

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `GetUserDTO` object. |
| `401 Unauthorized` | No Token | The request lacks a valid `Authorization` header or the token is expired. |

*Example `200 OK` Response (`GetUserDTO`):*
```json
{
  "userId": 0,
  "username": "string",
  "firstName": "string",
  "secondName": "string",
  "lastName": "string",
  "gender": true,
  "email": "string",
  "notes": "string",
  "userPoints": 0
}
```
---
### Update User
<a id="update-user"></a>

> `PATCH /api/v1/users`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `PATCH` | `/api/v1/users` | **Required** |

**Description**
Allows the authenticated user to update their own profile details using the JSON Patch standard (RFC 6902). This method is efficient for making partial updates, as you only need to send the fields that are changing.

**Request Body** (Array of `Operation` objects)
```json
[
  {
    "operationType": 0,
    "path": "string",
    "op": "string",
    "from": "string",
    "value": {}
  }
]
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | The updated `SendUserDTO` object. |
| `400 Bad Request` | Invalid Patch | The JSON Patch document was malformed or tried to update a non-existent/protected field. |
| `401 Unauthorized` | No Token | The request lacks a valid `Authorization` header. |

*Example `200 OK` Response (`SendUserDTO`):*
```json
{
  "userId": 0,
  "username": "string",
  "person": {
    "firstName": "string",
    "secondName": "string",
    "lastName": "string",
    "address": "string",
    "gender": true,
    "countryID": 0,
    "dateOfBirth": "2024-05-21T12:00:00Z",
    "email": "string",
    "notes": "string",
    "preferredLanguageID": 0,
    "personID": 0,
    "joinedDate": "2024-05-21T12:00:00Z"
  }
}
```
---
### User Logout
<a id="user-logout"></a>

> `POST /api/v1/users/logout`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/users/logout` | **Required** |

**Description**
To securely log a user out, the client application must call this endpoint to revoke the current `refreshToken`. This prevents it from being used to generate new access tokens in the future. After calling this, the client should also discard the `accessToken` and `refreshToken` locally.

**Request Body** (`RefreshTokenDTO`)
```json
{
  "token": "string"
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | Empty response body, confirming the token has been invalidated. |
| `400 Bad Request` | Invalid Token | The token was malformed or already invalid. |
| `401 Unauthorized` | No Token | The request lacks a valid `Authorization` header. |

---
---

# Collections Controller
<a id="collections-controller"></a>

The Collections API is the central hub for managing question collections. A "collection" can be a quiz, a test, or any group of related questions.

---
### Create Collection
<a id="create-collection"></a>

> `POST /api/v1/collections`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/collections` | **Required** |

**Description**
This is the primary endpoint for creating new content. It allows you to create a new question collection, including all of its nested questions and their respective choices, in a single atomic operation.

**Request Body** (`CreateQCollectionDTO`)
```json
{
  "collectionName": "string",
  "description": "string",
  "isPublic": true,
  "collecQuestions": [
    {
      "questionText": "string",
      "isMCQ": true,
      "questionPoints": 0,
      "rank": 0,
      "choices": [
        {
          "choiceText": "string",
          "isRightAnswer": true,
          "rank": 0
        }
      ]
    }
  ]
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An `integer` representing the unique ID of the new collection. |
| `401 Unauthorized` | No Token | The request lacks a valid `Authorization` header. |

*Example `200 OK` Response:*
```json
0
```
---
### Get All Collections
<a id="get-all-collections"></a>

> `GET /api/v1/collections`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/collections` | Optional |

**Description**
Fetches a lightweight list of all public collections. If the user is authenticated, this may also include their private collections. This "thumbnail" view is optimized for fast loading of list or browse pages.

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendCollectionDTO_Thumb` objects. |

*Example `200 OK` Response (Array of `SendCollectionDTO_Thumb`):*
```json
[
  {
    "collectionName": "string",
    "description": "string",
    "isPublic": true,
    "collectionID": 0,
    "addedTime": "2024-05-21T12:00:00Z",
    "categories": [
      {
        "categoryName": "string"
      }
    ]
  }
]
```
---
### Update Collection
<a id="update-collection"></a>

> `PATCH /api/v1/collections`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `PATCH` | `/api/v1/collections` | **Required** |

**Description**
This method is ideal for making partial updates to a collection's metadata (like its name or description). It uses the JSON Patch standard. Only the collection's owner can perform this action.

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CollecID` | integer | Yes | The unique ID of the collection to be updated. |

**Request Body** (Array of `Operation` objects)
```json
[
  {
    "operationType": 0,
    "path": "string",
    "op": "string",
    "from": "string",
    "value": {}
  }
]
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | The fully updated `SendCollectionDTO_Full` object. |
| `403 Forbidden` | Not Owner | The authenticated user is not the owner of the collection. |
| `404 Not Found` | Not Found | The collection with the specified ID does not exist. |

*Example `200 OK` Response (`SendCollectionDTO_Full`):*
```json
{
  "collectionName": "string",
  "description": "string",
  "isPublic": true,
  "collectionID": 0,
  "addedTime": "2024-05-21T12:00:00Z",
  "likes": 0,
  "disLikes": 0,
  "collecQuestions": [
    {
      "questionID": 0,
      "questionText": "string",
      "userID": 0,
      "isMCQ": true,
      "addedDate": "2024-05-21T12:00:00Z",
      "questionPoints": 0,
      "rank": 0,
      "choices": [
        {
          "choiceID": 0,
          "questionID": 0,
          "choiceText": "string",
          "rank": 0
        }
      ]
    }
  ]
}
```
---
### Delete Collection
<a id="delete-collection"></a>

> `DELETE /api/v1/collections`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `DELETE` | `/api/v1/collections` | **Required** |

**Description**
Permanently deletes a collection and all its associated content (questions, choices, submissions, etc.). This is a destructive and irreversible action. Only the owner of the collection can delete it.

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CollecID` | integer | Yes | The unique ID of the collection to be deleted. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An `integer` representing the ID of the deleted collection. |
| `403 Forbidden` | Not Owner | The authenticated user is not the owner of the collection. |
| `404 Not Found` | Not Found | The collection with the specified ID does not exist. |

*Example `200 OK` Response:*
```json
0
```
---
### Get Collection by ID
<a id="get-collection-by-id"></a>

> `GET /api/v1/collections/{CollecID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/collections/{CollecID}` | **Conditional** |

**Description**
Retrieves a single, complete collection by its unique ID, including all its questions and their choices. This is the primary endpoint to get all data needed to display or administer a quiz. If the collection is public, no authentication is needed. If it's private, the request must be authenticated by the collection's owner.

**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CollecID` | integer | Yes | The unique ID of the collection. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `SendCollectionDTO_Full` object. |
| `404 Not Found` | Not Found | The collection with the specified ID does not exist. |

*Example `200 OK` Response (`SendCollectionDTO_Full`):*
```json
{
  "collectionName": "string",
  "description": "string",
  "isPublic": true,
  "collectionID": 0,
  "addedTime": "2024-05-21T12:00:00Z",
  "likes": 0,
  "disLikes": 0,
  "collecQuestions": [
    {
      "questionID": 0,
      "questionText": "string",
      "userID": 0,
      "isMCQ": true,
      "addedDate": "2024-05-21T12:00:00Z",
      "questionPoints": 0,
      "rank": 0,
      "choices": [
        {
          "choiceID": 0,
          "questionID": 0,
          "choiceText": "string",
          "rank": 0
        }
      ]
    }
  ]
}
```
---
### Search Collections
<a id="search-collections"></a>

> `GET /api/v1/collections/search`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/collections/search` | Optional |

**Description**
This endpoint powers the public search functionality, allowing users to discover new collections by searching against fields like `collectionName` and `description`. It supports pagination.

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `SearchText` | string | No | The text to search for. If omitted, it may return recent public collections. |
| `PageNumber` | integer | No | The page number for pagination (e.g., `1`, `2`, `3`...). |
| `PageSize` | integer | No | The number of results to return per page. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendCollectionDTO_Search` objects. |

*Example `200 OK` Response (Array of `SendCollectionDTO_Search`):*
```json
[
  {
    "collectionName": "string",
    "description": "string",
    "isPublic": true,
    "collectionID": 0,
    "addedTime": "2024-05-21T12:00:00Z"
  }
]
```
---
### Get Collections by User
<a id="get-collections-by-user"></a>

> `GET /api/v1/collections/users/{UserID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/collections/users/{UserID}` | Optional |

**Description**
Use this to populate a user's public profile page with a list of all the public collections they have created. It returns a "thumbnail" version of each collection, which is a lighter payload suitable for list views.

**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `UserID` | integer | Yes | The unique ID of the user whose collections to fetch. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendCollectionDTO_Thumb` objects. |
| `404 Not Found` | Not Found | The user with the specified ID does not exist. |

*Example `200 OK` Response (Array of `SendCollectionDTO_Thumb`):*
```json
[
  {
    "collectionName": "string",
    "description": "string",
    "isPublic": true,
    "collectionID": 0,
    "addedTime": "2024-05-21T12:00:00Z",
    "categories": [
      {
        "categoryName": "string"
      }
    ]
  }
]
```
---
---

# Collection Likes Controller
<a id="collection-likes-controller"></a>

Manages user likes and dislikes on collections.

---
### Like or Dislike a Collection
<a id="like-or-dislike-a-collection"></a>

> `POST /api/v1/collections/likes/Like`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/collections/likes/Like` | **Required** |

**Description**
Allows an authenticated user to cast a "like" or "dislike" vote on a collection. The system typically handles toggling the vote (e.g., if a user has already liked a collection and sends another "like" request, it may remove the like).

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CollectionId` | integer | Yes | The ID of the collection to vote on. |
| `IsLike` | boolean | Yes | `true` for a like, `false` for a dislike. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | Empty response body, confirming the action was recorded. |
| `401 Unauthorized`| No Token | The request lacks a valid `Authorization` header. |
| `404 Not Found` | Not Found | The specified collection does not exist. |

---
---

# Collection Reviews Controller
<a id="collection-reviews-controller"></a>

Manages user-submitted reviews and ratings for collections.

---
### Create a Review
<a id="create-a-review"></a>

> `POST /api/v1/collections/reviews`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/collections/reviews` | **Required** |

**Description**
Allows an authenticated user to post a review for a collection. A user can typically only review a collection once.

**Request Body** (`MainCollectionsReviewDTO`)
```json
{
  "collectionID": 0,
  "userID": 0,
  "reviewText": "string",
  "reviewValue": 0,
  "reviewDate": "2024-05-21T12:00:00Z"
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | Empty response body. |
| `400 Bad Request` | Already Reviewed | The user has already submitted a review for this collection. |
| `401 Unauthorized`| No Token | The request lacks a valid `Authorization` header. |

---
### Update a Review
<a id="update-a-review"></a>

> `PATCH /api/v1/collections/reviews`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `PATCH` | `/api/v1/collections/reviews` | **Required** |

**Description**
Allows a user to update their own existing review for a collection using the JSON Patch standard.

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CollectionID` | integer | Yes | The ID of the collection whose review is being updated. The user must have a review for this collection. |

**Request Body** (Array of `Operation` objects)
```json
[
  {
    "operationType": 0,
    "path": "string",
    "op": "string",
    "from": "string",
    "value": {}
  }
]
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | The updated `MainCollectionsReviewDTO` object. |
| `403 Forbidden` | Not Owner | The user is trying to update a review that is not theirs. |
| `404 Not Found` | Not Found | The user has not submitted a review for this collection. |

*Example `200 OK` Response (`MainCollectionsReviewDTO`):*
```json
{
  "collectionID": 0,
  "userID": 0,
  "reviewText": "string",
  "reviewValue": 0,
  "reviewDate": "2024-05-21T12:00:00Z"
}
```
---
### Delete a Review
<a id="delete-a-review"></a>

> `DELETE /api/v1/collections/reviews`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `DELETE` | `/api/v1/collections/reviews` | **Required** |

**Description**
Allows a user to delete their own review for a collection.

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CollectionID` | integer | Yes | The ID of the collection whose review is being deleted. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | Empty response body. |
| `403 Forbidden` | Not Owner | The user is trying to delete a review that is not theirs. |
| `404 Not Found` | Not Found | The user has not submitted a review for this collection. |

---
### Get Reviews for a Collection
<a id="get-reviews-for-a-collection"></a>

> `GET /api/v1/collections/reviews/{CollectionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/collections/reviews/{CollectionID}` | Optional |

**Description**
Retrieves all reviews for a specific collection, with support for pagination.

**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CollectionID` | integer | Yes | The ID of the collection. |

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `Page` | integer | No | The page number for pagination (e.g., `1`, `2`, `3`...). |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `MainCollectionsReviewDTO` objects. |
| `404 Not Found` | Not Found | The specified collection does not exist. |

*Example `200 OK` Response (Array of `MainCollectionsReviewDTO`):*
```json
[
  {
    "collectionID": 0,
    "userID": 0,
    "reviewText": "string",
    "reviewValue": 0,
    "reviewDate": "2024-05-21T12:00:00Z"
  }
]
```
---
---

# Collection Submissions Controller
<a id="collection-submissions-controller"></a>

Manages the lifecycle of a user attempting a collection (e.g., taking a quiz).

---
### Create a Submission
<a id="create-a-submission"></a>

> `POST /api/v1/submitions`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/submitions` | **Required** |

**Description**
Before a user starts answering questions in a collection, call this endpoint to create a "submission" container. The returned `submitionID` is crucial and must be used to associate all subsequent answers with this specific attempt.

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CollectionID` | integer | Yes | The ID of the collection being attempted. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An `integer` representing the new `submitionID`. |
| `401 Unauthorized`| No Token | The request lacks a valid `Authorization` header. |
| `404 Not Found` | Not Found | The specified collection does not exist. |

*Example `200 OK` Response:*
```json
0
```
---
### Get Submission Details
<a id="get-submission-details"></a>

> `GET /api/v1/submitions`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/submitions` | **Required** |

**Description**
Retrieves the details and final results of a specific submission attempt after it has been completed.

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `SubmissionID` | integer | Yes | The ID of the submission to retrieve, obtained from the [Create a Submission](#create-a-submission) endpoint. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `CollectionSubmissionMainDTO` object. |
| `403 Forbidden` | Not Owner | You can only view your own submissions. |
| `404 Not Found` | Not Found | The specified submission does not exist. |

*Example `200 OK` Response (`CollectionSubmissionMainDTO`):*
```json
{
  "submitDate": "2024-05-21T12:00:00Z",
  "username": "string",
  "totalChosenChoices": 0,
  "totalRightAnswers": 0
}
```
---
### Delete a Submission
<a id="delete-a-submission"></a>

> `DELETE /api/v1/submitions`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `DELETE` | `/api/v1/submitions` | **Required** |

**Description**
Deletes a user's submission record. This might be used to allow a user to clear their attempt history.

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `SubmitionID` | integer | Yes | The ID of the submission to delete. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | Empty response body. |
| `403 Forbidden` | Not Owner | You can only delete your own submissions. |
| `404 Not Found` | Not Found | The specified submission does not exist. |

---
---

# Questions Controller
<a id="questions-controller"></a>

Manages the questions that belong to collections.

---
### Add Questions to Collection
<a id="add-questions-to-collection"></a>

> `POST /api/v1/questions`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/questions` | **Required** |

**Description**
Use this endpoint to add one or more new questions to a collection that has already been created. This is useful for building a collection incrementally.

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CollectionID` | integer | Yes | The ID of the collection to add questions to. |

**Request Body** (Array of `CreateQuestionDTO` objects)
```json
[
  {
    "questionText": "string",
    "isMCQ": true,
    "questionPoints": 0,
    "rank": 0,
    "choices": [
      {
        "choiceText": "string",
        "isRightAnswer": true,
        "rank": 0
      }
    ]
  }
]
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendQuestionDTO` for the newly created questions. |
| `403 Forbidden` | Not Owner | You do not own the collection you are trying to modify. |

*Example `200 OK` Response (Array of `SendQuestionDTO`):*
```json
[
  {
    "questionID": 0,
    "questionText": "string",
    "userID": 0,
    "isMCQ": true,
    "addedDate": "2024-05-21T12:00:00Z",
    "questionPoints": 0,
    "rank": 0,
    "choices": [
      {
        "choiceID": 0,
        "questionID": 0,
        "choiceText": "string",
        "rank": 0
      }
    ]
  }
]
```
---
### Get Questions from Collection
<a id="get-questions-from-collection"></a>

> `GET /api/v1/questions`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/questions` | Optional |

**Description**
Retrieves all questions for a given collection. This is a simplified endpoint and may not be the primary way to fetch questions for a quiz (see `GET /api/v1/collections/{CollecID}`).

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CollectionID` | integer | Yes | The ID of the collection whose questions to fetch. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `SendQuestionDTO` object (Note: The schema suggests a single object, which might be an error. It typically should be an array). |
| `404 Not Found` | Not Found | The specified collection does not exist. |

*Example `200 OK` Response (`SendQuestionDTO`):*
```json
{
  "questionID": 0,
  "questionText": "string",
  "userID": 0,
  "isMCQ": true,
  "addedDate": "2024-05-21T12:00:00Z",
  "questionPoints": 0,
  "rank": 0,
  "choices": [
    {
      "choiceID": 0,
      "questionID": 0,
      "choiceText": "string",
      "rank": 0
    }
  ]
}
```
---
### Delete a Question
<a id="delete-a-question"></a>

> `DELETE /api/v1/questions`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `DELETE` | `/api/v1/questions` | **Required** |

**Description**
Permanently deletes a question and its associated choices.

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionID` | integer | Yes | The ID of the question to delete. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An `integer` representing the ID of the deleted question. |
| `403 Forbidden` | Not Owner | You do not own the question. |
| `404 Not Found` | Not Found | The specified question does not exist. |

*Example `200 OK` Response:*
```json
0
```
---
### Get a Single Question
<a id="get-a-single-question"></a>

> `GET /api/v1/questions/{QuestionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/questions/{QuestionID}` | Optional |

**Description**
Retrieves a single question by its unique ID, including its choices.

**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionID` | integer | Yes | The unique ID of the question. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `SendQuestionDTO` object. |
| `404 Not Found` | Not Found | A question with the specified ID was not found. |

*Example `200 OK` Response (`SendQuestionDTO`):*
```json
{
  "questionID": 0,
  "questionText": "string",
  "userID": 0,
  "isMCQ": true,
  "addedDate": "2024-05-21T12:00:00Z",
  "questionPoints": 0,
  "rank": 0,
  "choices": [
    {
      "choiceID": 0,
      "questionID": 0,
      "choiceText": "string",
      "rank": 0
    }
  ]
}
```
---
### Update a Question
<a id="update-a-question"></a>

> `PATCH /api/v1/questions/{QuestionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `PATCH` | `/api/v1/questions/{QuestionID}` | **Required** |

**Description**
Updates specific fields of an existing question (e.g., its text or rank) using the JSON Patch standard.

**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionID` | integer | Yes | The unique ID of the question to update. |

**Request Body** (Array of `Operation` objects)
```json
[
  {
    "operationType": 0,
    "path": "string",
    "op": "string",
    "from": "string",
    "value": {}
  }
]
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | The updated `SendQuestionDTO` object. |
| `403 Forbidden` | Not Owner | You do not own the question you are trying to modify. |

*Example `200 OK` Response (`SendQuestionDTO`):*
```json
{
  "questionID": 0,
  "questionText": "string",
  "userID": 0,
  "isMCQ": true,
  "addedDate": "2024-05-21T12:00:00Z",
  "questionPoints": 0,
  "rank": 0,
  "choices": [
    {
      "choiceID": 0,
      "questionID": 0,
      "choiceText": "string",
      "rank": 0
    }
  ]
}
```
---
### Update Question Points
<a id="update-question-points"></a>

> `PATCH /api/v1/questions/points/{QuestionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `PATCH` | `/api/v1/questions/points/{QuestionID}` | **Required** |

**Description**
A dedicated, lightweight endpoint to quickly update the point value of a question.

**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionID` | integer | Yes | The ID of the question to update. |

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `NewPointsVal` | integer | Yes | The new point value for the question. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An `integer` representing the updated point value. |
| `403 Forbidden` | Not Owner | You do not own the question. |

*Example `200 OK` Response:*
```json
0
```
---
### Get Random Questions
<a id="get-random-questions"></a>

> `GET /api/v1/questions/random`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/questions/random` | Optional |

**Description**
Fetches a set of random questions from a specific collection. This is useful for creating a dynamic quiz experience where the questions are not always in the same order or for sampling a large collection.

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `collectionId` | integer | Yes | The ID of the collection to pull random questions from. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `QuestionWithChoicesDto` objects. This DTO is lightweight and does not include the `isRightAnswer` field. |
| `404 Not Found` | Not Found | The specified collection does not exist. |

*Example `200 OK` Response (Array of `QuestionWithChoicesDto`):*
```json
[
  {
    "questionID": 0,
    "questionText": "string",
    "rank": 0,
    "choices": [
      {
        "choiceID": 0,
        "choiceText": "string"
      }
    ]
  }
]
```
---
---

# Choices Controller
<a id="choices-controller"></a>

Manages the answer choices for questions.

---
### Add Choices to Question
<a id="add-choices-to-question"></a>

> `POST /api/v1/choices`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/choices` | **Required** |

**Description**
Creates one or more new choices and associates them with an existing question.

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionID` | integer | Yes | The ID of the question to add these choices to. |

**Request Body** (Array of `CreateChoiceDTO` objects)
```json
[
  {
    "choiceText": "string",
    "isRightAnswer": true,
    "rank": 0
  }
]
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendChoiceDTO` for the newly created choices. |
| `400 Bad Request` | Invalid Input | The question ID may be missing or invalid. |
| `401 Unauthorized`| No Token | The request lacks a valid `Authorization` header. |

*Example `200 OK` Response (Array of `SendChoiceDTO`):*
```json
[
  {
    "choiceID": 0,
    "questionID": 0,
    "choiceText": "string",
    "rank": 0
  }
]
```
---
### Get Choices by IDs
<a id="get-choices-by-ids"></a>

> `GET /api/v1/choices`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/choices` | Optional |

**Description**
This endpoint allows you to fetch details for a specific list of choices in a single call by providing their IDs in the request body.

**Request Body** (Array of integers)
```json
[
  0
]
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A map where keys are question IDs and values are arrays of `SendChoiceDTO` objects, grouping the choices by their parent question. |

*Example `200 OK` Response:*
```json
{
  "additionalProp1": [
    {
      "choiceID": 0,
      "questionID": 0,
      "choiceText": "string",
      "rank": 0
    }
  ]
}
```
---
### Get Choices for a Question
<a id="get-choices-for-a-question"></a>

> `GET /api/v1/choices/questions/{questionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/choices/questions/{questionID}` | Optional |

**Description**
Retrieves all available choices for a specific question. This is the standard way to get the options to display to a user taking a quiz. The response does not indicate which answer is correct.

**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `questionID` | integer | Yes | The ID of the question. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendChoiceDTO` objects. |
| `404 Not Found` | Not Found | The question was not found. |

*Example `200 OK` Response (Array of `SendChoiceDTO`):*
```json
[
  {
    "choiceID": 0,
    "questionID": 0,
    "choiceText": "string",
    "rank": 0
  }
]
```
---
### Get Right Answers for a Question
<a id="get-right-answers-for-a-question"></a>

> `GET /api/v1/choices/answers/{questionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/choices/answers/{questionID}` | **Required** |

**Description**
This is a protected endpoint used for grading or showing the correct answer(s) after a user has submitted their choice. It retrieves only the choice(s) marked as correct for a given question.

**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `questionID` | integer | Yes | The ID of the question. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendChoiceDTO` objects that are the correct answers. |
| `401 Unauthorized`| No Token | The request lacks a valid `Authorization` header. |

*Example `200 OK` Response (Array of `SendChoiceDTO`):*
```json
[
  {
    "choiceID": 0,
    "questionID": 0,
    "choiceText": "string",
    "rank": 0
  }
]
```
---
### Get Choice with Explanation
<a id="get-choice-with-explanation"></a>

> `GET /api/v1/choices/explanation/{choiceId}/{questionId}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/choices/explanation/{choiceId}/{questionId}` | **Required** |

**Description**
After a user answers a question, use this endpoint to show them whether their choice was right or wrong, and provide the official explanation for the answer.

**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `choiceId` | integer | Yes | The ID of the choice the user selected. |
| `questionId` | integer | Yes | The ID of the question. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `SendChoiceWithExplanationDTO` object. |
| `401 Unauthorized`| No Token | The request lacks a valid `Authorization` header. |

*Example `200 OK` Response (`SendChoiceWithExplanationDTO`):*
```json
{
  "choiceID": 0,
  "choiceText": "string",
  "isRightAnswer": true,
  "explanationText": "string",
  "explanationID": 0
}
```
---
### Update a Choice
<a id="update-a-choice"></a>

> `PATCH /api/v1/choices/{ChoiceID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `PATCH` | `/api/v1/choices/{ChoiceID}` | **Required** |

**Description**
Updates specific fields of an existing choice (e.g., its text or rank) using the JSON Patch standard.

**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `ChoiceID` | integer | Yes | The ID of the choice to update. |

**Request Body** (Array of `Operation` objects)
```json
[
  {
    "operationType": 0,
    "path": "string",
    "op": "string",
    "from": "string",
    "value": {}
  }
]
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | The updated `SendChoiceDTO` object. |
| `403 Forbidden` | Not Owner | You do not own the parent question. |

*Example `200 OK` Response (`SendChoiceDTO`):*
```json
{
  "choiceID": 0,
  "questionID": 0,
  "choiceText": "string",
  "rank": 0
}
```
---
### Delete a Choice
<a id="delete-a-choice"></a>

> `DELETE /api/v1/choices/{ChoiceID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `DELETE` | `/api/v1/choices/{ChoiceID}` | **Required** |

**Description**
Permanently deletes a choice from a question.

**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `ChoiceID` | integer | Yes | The ID of the choice to delete. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An `integer` representing the ID of the deleted choice. |
| `403 Forbidden` | Not Owner | You do not own the parent question. |

*Example `200 OK` Response:*
```json
0
```
---
---

# Chosen Choices Controller
<a id="chosen-choices-controller"></a>

Manages the recording and retrieval of user answers during a submission attempt.

---
### Submit a Chosen Answer
<a id="submit-a-chosen-answer"></a>

> `POST /api/v1/choices/chosen`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/choices/chosen` | **Required** |

**Description**
As a user answers each question during a quiz, call this endpoint to log their selected choice. This links the user's answer to their specific submission attempt.

**Request Body** (`Add_chosen_choicesDTO`)
```json
{
  "choiceID": 0,
  "submitionID": 0
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `send_chosen_choicesDTO` object confirming the record was created. |
| `403 Forbidden` | Not Owner | You cannot add choices to someone else's submission. |

*Example `200 OK` Response (`send_chosen_choicesDTO`):*
```json
{
  "chosen_ChoiceID": 0,
  "choiceID": 0,
  "userID": 0,
  "chosenDate": "2024-05-21T12:00:00Z",
  "submitionID": 0
}
```
---
### Get Submitted Answers
<a id="get-submitted-answers"></a>

> `GET /api/v1/choices/chosen/submition/{submitionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/choices/chosen/submition/{submitionID}` | **Required** |

**Description**
Retrieves all the choices a user made for a given submission. This is useful for displaying a summary of a user's answers after a quiz is complete.

**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `submitionID` | integer | Yes | The ID of the submission. |

**Header Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionIDs` | Array of integers | No | An optional comma-separated list of question IDs to filter the results for. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A map where keys are question IDs and values are `send_chosen_choicesDTO` objects. |
| `403 Forbidden` | Not Owner | You can only view your own chosen choices. |

*Example `200 OK` Response:*
```json
{
  "additionalProp1": {
    "chosen_ChoiceID": 0,
    "choiceID": 0,
    "userID": 0,
    "chosenDate": "2024-05-21T12:00:00Z",
    "submitionID": 0
  }
}
```
---
---

# Explanations Controller
<a id="explanations-controller"></a>

Manages explanations for question answers.

---
### Create an Explanation
<a id="create-an-explanation"></a>

> `POST /api/v1/explanations`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/explanations` | **Required** |

**Description**
Creates an explanation for a question's answer. This text is shown to users to help them understand why an answer is correct.

**Request Body** (`AnswerExplanationMainDTO`)
```json
{
  "explanationText": "string",
  "questionID": 0
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `boolean` (`true`) indicating success. |
| `403 Forbidden` | Not Owner | You can only add explanations to your own questions. |

*Example `200 OK` Response:*
```json
true
```
---
### Get Explanations for a Question
<a id="get-explanations-for-a-question"></a>

> `GET /api/v1/explanations/questions/{QuestionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/explanations/questions/{QuestionID}` | Optional |

**Description**
Retrieves all explanations associated with a given question.

**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionID` | integer | Yes | The ID of the question. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `GetAnswerExplanationDTO` objects. |
| `404 Not Found` | Not Found | The specified question does not exist. |

*Example `200 OK` Response (Array of `GetAnswerExplanationDTO`):*
```json
[
  {
    "explanationText": "string",
    "questionID": 0,
    "addedDate": "2024-05-21T12:00:00Z"
  }
]
```
---
### Get a Single Explanation
<a id="get-a-single-explanation"></a>

> `GET /api/v1/explanations/{ExplainID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/explanations/{ExplainID}` | Optional |

**Description**
Retrieves a single explanation by its unique ID.

**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `ExplainID` | integer | Yes | The unique ID of the explanation. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `GetAnswerExplanationDTO` object. |
| `404 Not Found` | Not Found | The explanation was not found. |

*Example `200 OK` Response (`GetAnswerExplanationDTO`):*
```json
{
  "explanationText": "string",
  "questionID": 0,
  "addedDate": "2024-05-21T12:00:00Z"
}
```
---
---

# Categories Controller
<a id="categories-controller"></a>

Manages the creation and retrieval of categories used to classify content.

---
### Create a Category
<a id="create-a-category"></a>

> `POST /api/v1/categories`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/categories` | **Required** |

**Description**
Creates a new category that can be used to tag questions and collections. Category names are typically unique.

**Request Body** (`CreateCategoryDTO`)
```json
{
  "categoryName": "string"
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `SendCategoryDTO` object for the new category. |
| `400 Bad Request` | Already Exists | A category with that name already exists. |

*Example `200 OK` Response (`SendCategoryDTO`):*
```json
{
  "categoryID": 0,
  "categoryName": "string"
}
```
---
### Get Categories
<a id="get-categories"></a>

> `GET /api/v1/categories/{RowsCount}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/categories/{RowsCount}` | Optional |

**Description**
Retrieves a list of categories, with optional filtering by name and a limit on the number of results. Useful for populating a category selector or search autocomplete.

**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `RowsCount` | integer | Yes | The maximum number of categories to return. |

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CategorySumName` | string | No | A search string to filter category names (e.g., "Phys" would match "Physics"). |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendCategoryDTO` objects. |

*Example `200 OK` Response (Array of `SendCategoryDTO`):*
```json
[
  {
    "categoryID": 0,
    "categoryName": "string"
  }
]
```
---
---

# Question Categories Controller
<a id="question-categories-controller"></a>

Manages the relationship between questions and categories.

---
### Get Categories for a Question
<a id="get-categories-for-a-question"></a>

> `GET /api/v1/categories/questions/{QuestionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/categories/questions/{QuestionID}` | Optional |

**Description**
Retrieves all categories that have been associated with a specific question.

**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionID` | integer | Yes | The ID of the question. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendQuestionsCategoryDTO` objects. |
| `404 Not Found` | Not Found | The specified question does not exist. |

*Example `200 OK` Response (Array of `SendQuestionsCategoryDTO`):*
```json
[
  {
    "question_CategoryID": 0,
    "questionID": 0,
    "categoryID": 0,
    "categoryName": "string"
  }
]
```
---
### Add Categories to a Question
<a id="add-categories-to-a-question"></a>

> `POST /api/v1/categories/questions/{QuestionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/categories/questions/{QuestionID}` | **Required** |

**Description**
Associates one or more existing categories with a question. This "tags" the question, making it discoverable.

**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionID` | integer | Yes | The ID of the question to add categories to. |

**Request Body** (Array of `CreateQuestionsCategoryDTO` objects)
```json
[
  {
    "categoryID": 0
  }
]
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An `integer` representing the number of new associations created. |
| `403 Forbidden` | Not Owner | You do not own the question. |

*Example `200 OK` Response:*
```json
0
```
---
### Delete All Categories from a Question
<a id="delete-all-categories-from-a-question"></a>

> `DELETE /api/v1/categories/questions/{QuestionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `DELETE` | `/api/v1/categories/questions/{QuestionID}` | **Required** |

**Description**
Removes all category associations from a single question. This is a bulk "untag" operation.

**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionID` | integer | Yes | The ID of the question to remove all category links from. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An `integer` representing the number of associations removed. |
| `403 Forbidden` | Not Owner | You do not own the question. |

*Example `200 OK` Response:*
```json
0
```
---
### Delete a Question-Category Link
<a id="delete-a-question-category-link"></a>

> `DELETE /api/v1/categories/{QuestionCategoryID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `DELETE` | `/api/v1/categories/{QuestionCategoryID}` | **Required** |

**Description**
Removes a single, specific association between a question and a category. This does not delete the question or the category itself, only the link between them.

**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionCategoryID` | integer | Yes | The unique ID of the question-category link (the `question_CategoryID` from the `SendQuestionsCategoryDTO` object). |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `boolean` (`true`) indicating success. |
| `403 Forbidden` | Not Owner | You do not own the question. |
| `404 Not Found` | Not Found | The specified question-category link does not exist. |

*Example `200 OK` Response:*
```json
true
```
---
---

# Home Screen Controller
<a id="home-screen-controller"></a>

Provides aggregated data suitable for populating a main home screen or dashboard.

---
### Get Top Collections
<a id="get-top-collections"></a>

> `GET /api/v1/home/collections/top`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/home/collections/top` | Optional |

**Description**
This endpoint is designed to populate a "featured" or "top" section on a home screen by retrieving a list of top-rated or most popular collections.

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendCollectionDTO_Thumb` objects. |

*Example `200 OK` Response (Array of `SendCollectionDTO_Thumb`):*
```json
[
  {
    "collectionName": "string",
    "description": "string",
    "isPublic": true,
    "collectionID": 0,
    "addedTime": "2024-05-21T12:00:00Z",
    "categories": [
      {
        "categoryName": "string"
      }
    ]
  }
]
```
---
### Get Top Followers
<a id="get-top-followers"></a>

> `GET /api/v1/home/followers/top`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/home/followers/top` | Optional |

**Description**
This endpoint is designed to populate a "leaderboard" or "top creators" section on a home screen by retrieving a list of top users (e.g., by points or number of followers).

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendUserDTO` objects. |

*Example `200 OK` Response (Array of `SendUserDTO`):*
```json
[
  {
    "userId": 0,
    "username": "string",
    "person": {
      "firstName": "string",
      "secondName": "string",
      "lastName": "string",
      "address": "string",
      "gender": true,
      "countryID": 0,
      "dateOfBirth": "2024-05-21T12:00:00Z",
      "email": "string",
      "notes": "string",
      "preferredLanguageID": 0,
      "personID": 0,
      "joinedDate": "2024-05-21T12:00:00Z"
    }
  }
]
```
---
---

# Health & Test Controller
<a id="health--test-controller"></a>

Endpoints for system health checks and internal/developer testing. These endpoints are generally not for public client consumption.

---
### Health Check
<a id="health-check"></a>

> `GET /api/health`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/health` | Not Required |

**Description**
A simple endpoint to verify that the API service is running and reachable. It can be used by monitoring services to check application health.

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | Empty response body, indicating the service is healthy. |

---
### Get Secure Metrics
<a id="get-secure-metrics"></a>

> `GET /api/secure-metrics`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/secure-metrics` | **Required** (Likely Admin-only) |

**Description**
An authenticated endpoint for retrieving system metrics. This is likely restricted to administrative users.

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | Varies. Likely a JSON or text-based metrics report. |
| `401 Unauthorized`| No Token | The request lacks a valid `Authorization` header. |
| `403 Forbidden` | Insufficient Permissions | The user does not have the required permissions to view metrics. |

---
### Get Claims (Test)
<a id="get-claims-test"></a>

> `GET /API/Test/GetClaims`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/API/Test/GetClaims` | **Required** |

**Description**
A test endpoint for developers to inspect the claims contained within their current `accessToken`.

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A representation of the JWT claims. |

---
### Create New Token (Test)
<a id="create-new-token-test"></a>

> `POST /API/Test/CreateNewToken`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/API/Test/CreateNewToken` | **Required** (Likely Admin-only) |

**Description**
A test endpoint for developers to generate a new token for a specific user ID, bypassing the normal login flow.

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `userID` | integer | Yes | The ID of the user to generate a token for. |

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A new token. |

---
### Upload Image (Test)
<a id="upload-image-test"></a>

> `POST /API/Test/UploadImage`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/API/Test/UploadImage` | **Required** |

**Description**
A test endpoint for developers to test the file upload functionality.

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `folderPath` | string | No | The destination folder path on the server. |
| `fileName` | string | No | The desired file name on the server. |

**Request Body** (`multipart/form-data`)
The request must be a multipart form data request containing the file to be uploaded.

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | Empty response body. |

---
### Test Critical Log (Test)
<a id="test-critical-log-test"></a>

> `GET /API/Test/TestCriticalLog`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/API/Test/TestCriticalLog` | **Required** (Likely Admin-only) |

**Description**
A test endpoint for developers to trigger a critical-level log event to ensure the logging and monitoring systems are working correctly.

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | Empty response body. |

---
### External Login (Test)
<a id="external-login-test"></a>

> `POST /API/Test/external-login`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/API/Test/external-login` | Not Required |

**Description**
A test endpoint for developers to simulate an external login flow.

**Request Body** (`ExternalLoginRequestDTO`)
```json
{
  "provider": "string",
  "idToken": "string"
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | Varies based on test implementation. |

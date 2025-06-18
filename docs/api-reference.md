# 📘 Q&A Service - Full API Documentation

This document provides a complete reference to all RESTful API endpoints in the Q&A Service.

---

## 🔐 Auth

### POST /api/auth/refresh-token
**Description**: Refresh an expired access token using a valid refresh token.  
**Request Body**:
```json
{
  "token": "string"
}
```
**Response**:
```json
{
  "accessToken": "string",
  "refreshToken": "string"
}
```

---

## 👤 Users

### POST /api/v1/users/signup
**Description**: Register a new user.  
**Request Body**:
```json
{
  "username": "string",
  "password": "string",
  "person": {
    "firstName": "string",
    "lastName": "string",
    "email": "string"
  }
}
```

### POST /api/v1/users/login
**Description**: Log in with credentials.  
**Request Body**:
```json
{
  "username": "string",
  "password": "string"
}
```

### POST /api/v1/users/logout
**Description**: Log out and invalidate refresh token.  
**Request Body**:
```json
{
  "token": "string"
}
```

---

## 🏛️ Institutions

_No dedicated endpoints found under "Institutions" in Swagger. Please add if needed._

---

## 🎯 Choices

### POST /api/v1/choices?QuestionID={id}
**Description**: Add choices to a question.

### GET /api/v1/choices/questions/{questionID}
**Description**: Get all choices for a specific question.

### GET /api/v1/choices/answers/{questionID}
**Description**: Get the correct answer for a question.

### PATCH /api/v1/choices/{ChoiceID}
**Description**: Update a choice using a JSON Patch array.

### DELETE /api/v1/choices/{ChoiceID}
**Description**: Delete a choice by ID.

### GET /api/v1/choices/explanation/{choiceId}/{questionId}
**Description**: Get explanation for a specific choice of a question.

---

## ❓ Questions

### POST /api/v1/questions?CollectionID={id}
**Description**: Add questions to a collection.

### GET /api/v1/questions?CollectionID={id}
**Description**: Get all questions for a collection.

### GET /api/v1/questions/{QuestionID}
**Description**: Get question details by ID.

### PATCH /api/v1/questions/{QuestionID}
**Description**: Update question fields using JSON Patch.

### PATCH /api/v1/questions/points/{QuestionID}?NewPointsVal={val}
**Description**: Update the points value of a question.

### DELETE /api/v1/questions?QuestionID={id}
**Description**: Delete a question by ID.

### GET /api/v1/questions/random?collectionId={id}
**Description**: Get random questions with choices for a collection.

---

## 📚 Collections

### POST /api/v1/collections
**Description**: Create a new collection with optional questions.

### GET /api/v1/collections
**Description**: List all collections.

### GET /api/v1/collections/{CollecID}
**Description**: Get a collection by ID.

### GET /api/v1/collections/users/{UserID}
**Description**: Get collections owned by a user.

### PATCH /api/v1/collections?CollecID={id}
**Description**: Update a collection using JSON Patch.

### DELETE /api/v1/collections?CollecID={id}
**Description**: Delete a collection by ID.

### GET /api/v1/collections/search?SearchText=...&PageNumber=1&PageSize=10
**Description**: Search collections with pagination.

---

### 💬 Collection Reviews

### POST /api/v1/collections/reviews
**Description**: Add a review to a collection.

### GET /api/v1/collections/reviews/{CollectionID}?Page=1
**Description**: Get paginated reviews of a collection.

### PATCH /api/v1/collections/reviews?CollectionID={id}
**Description**: Update a review using JSON Patch.

### DELETE /api/v1/collections/reviews?CollectionID={id}
**Description**: Delete a review.

---

### ❤️ Collection Likes

### POST /api/v1/collections/likes/Like?CollectionId={id}&IsLike=true
**Description**: Like or dislike a collection.

---

### 📤 Submitions

### POST /api/v1/submitions?CollectionID={id}
**Description**: Submit answers for a collection.

### GET /api/v1/submitions?SubmissionID={id}
**Description**: Get submition result.

### DELETE /api/v1/submitions?SubmitionID={id}
**Description**: Delete a submition.

---

### ✅ Chosen Choices

### POST /api/v1/choices/chosen
**Description**: Submit selected choices for a submition.

### GET /api/v1/choices/chosen/submition/{submitionID}
**Description**: Get chosen choices for a submition.

---

## 🏠 Home Screen

### GET /api/v1/home/collections/top
**Description**: Get top collections for homepage.

### GET /api/v1/home/followers/top
**Description**: Get top followed users.

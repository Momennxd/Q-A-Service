
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

#### Request Structure
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

#### Example Request
**Request Body**
```json
{
  "collectionName": "Introduction to Astronomy",
  "description": "A collection of basic questions about our solar system and beyond.",
  "isPublic": true,
  "collecQuestions": [
    {
      "questionText": "Which planet is known as the Red Planet?",
      "isMCQ": true,
      "questionPoints": 10,
      "rank": 1,
      "choices": [
        {
          "choiceText": "Venus",
          "isRightAnswer": false,
          "rank": 1
        },
        {
          "choiceText": "Mars",
          "isRightAnswer": true,
          "rank": 2
        },
        {
          "choiceText": "Jupiter",
          "isRightAnswer": false,
          "rank": 3
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

#### Response Structure
**`200 OK` Response Body**
```
integer
```

#### Example Response
**`200 OK` Response Body**
```json
101
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

#### Response Structure
**`200 OK` Response Body** (Array of `SendCollectionDTO_Thumb`)
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

#### Example Response
**`200 OK` Response Body**
```json
[
  {
    "collectionName": "Introduction to Astronomy",
    "description": "A collection of basic questions about our solar system and beyond.",
    "isPublic": true,
    "collectionID": 101,
    "addedTime": "2024-05-21T13:00:00Z",
    "categories": [
      {
        "categoryName": "Science"
      }
    ]
  },
  {
    "collectionName": "World War II History",
    "description": "Key events and figures from WWII.",
    "isPublic": true,
    "collectionID": 102,
    "addedTime": "2024-05-20T10:00:00Z",
    "categories": [
      {
        "categoryName": "History"
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

#### Request Structure
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

#### Example Request
**Request URL**
`/api/v1/collections?CollecID=101`

**Request Body**
```json
[
  {
    "op": "replace",
    "path": "/description",
    "value": "An updated, more detailed description of the astronomy collection."
  }
]
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | The fully updated `SendCollectionDTO_Full` object. |
| `403 Forbidden` | Not Owner | The authenticated user is not the owner of the collection. |
| `404 Not Found` | Not Found | The collection with the specified ID does not exist. |

#### Response Structure
**`200 OK` Response Body** (`SendCollectionDTO_Full`)
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

#### Example Response
**`200 OK` Response Body**
```json
{
  "collectionName": "Introduction to Astronomy",
  "description": "An updated, more detailed description of the astronomy collection.",
  "isPublic": true,
  "collectionID": 101,
  "addedTime": "2024-05-21T13:00:00Z",
  "likes": 50,
  "disLikes": 2,
  "collecQuestions": [
    {
      "questionID": 201,
      "questionText": "Which planet is known as the Red Planet?",
      "userID": 123,
      "isMCQ": true,
      "addedDate": "2024-05-21T13:00:00Z",
      "questionPoints": 10,
      "rank": 1,
      "choices": [
        { "choiceID": 301, "questionID": 201, "choiceText": "Venus", "rank": 1 },
        { "choiceID": 302, "questionID": 201, "choiceText": "Mars", "rank": 2 },
        { "choiceID": 303, "questionID": 201, "choiceText": "Jupiter", "rank": 3 }
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

#### Request Structure
**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CollecID` | integer | Yes | The unique ID of the collection to be deleted. |

#### Example Request
**Request URL**
`/api/v1/collections?CollecID=102`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An `integer` representing the ID of the deleted collection. |
| `403 Forbidden` | Not Owner | The authenticated user is not the owner of the collection. |
| `404 Not Found` | Not Found | The collection with the specified ID does not exist. |

#### Response Structure
**`200 OK` Response Body**
```
integer
```

#### Example Response
**`200 OK` Response Body**
```json
102
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

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CollecID` | integer | Yes | The unique ID of the collection. |

#### Example Request
**Request URL**
`/api/v1/collections/101`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `SendCollectionDTO_Full` object. |
| `404 Not Found` | Not Found | The collection with the specified ID does not exist. |

#### Response Structure
**`200 OK` Response Body** (`SendCollectionDTO_Full`)
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

#### Example Response
**`200 OK` Response Body**
```json
{
  "collectionName": "Introduction to Astronomy",
  "description": "A collection of basic questions about our solar system and beyond.",
  "isPublic": true,
  "collectionID": 101,
  "addedTime": "2024-05-21T13:00:00Z",
  "likes": 50,
  "disLikes": 2,
  "collecQuestions": [
    {
      "questionID": 201,
      "questionText": "Which planet is known as the Red Planet?",
      "userID": 123,
      "isMCQ": true,
      "addedDate": "2024-05-21T13:00:00Z",
      "questionPoints": 10,
      "rank": 1,
      "choices": [
        { "choiceID": 301, "questionID": 201, "choiceText": "Venus", "rank": 1 },
        { "choiceID": 302, "questionID": 201, "choiceText": "Mars", "rank": 2 },
        { "choiceID": 303, "questionID": 201, "choiceText": "Jupiter", "rank": 3 }
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

#### Request Structure
**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `SearchText` | string | No | The text to search for. If omitted, it may return recent public collections. |
| `PageNumber` | integer | No | The page number for pagination (e.g., `1`, `2`, `3`...). |
| `PageSize` | integer | No | The number of results to return per page. |

#### Example Request
**Request URL**
`/api/v1/collections/search?SearchText=History&PageNumber=1&PageSize=10`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendCollectionDTO_Search` objects. |

#### Response Structure
**`200 OK` Response Body** (Array of `SendCollectionDTO_Search`)
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

#### Example Response
**`200 OK` Response Body**
```json
[
  {
    "collectionName": "World War II History",
    "description": "Key events and figures from WWII.",
    "isPublic": true,
    "collectionID": 102,
    "addedTime": "2024-05-20T10:00:00Z"
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

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `UserID` | integer | Yes | The unique ID of the user whose collections to fetch. |

#### Example Request
**Request URL**
`/api/v1/collections/users/123`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendCollectionDTO_Thumb` objects. |
| `404 Not Found` | Not Found | The user with the specified ID does not exist. |

#### Response Structure
**`200 OK` Response Body** (Array of `SendCollectionDTO_Thumb`)
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

#### Example Response
**`200 OK` Response Body**
```json
[
  {
    "collectionName": "Introduction to Astronomy",
    "description": "A collection of basic questions about our solar system and beyond.",
    "isPublic": true,
    "collectionID": 101,
    "addedTime": "2024-05-21T13:00:00Z",
    "categories": [
      {
        "categoryName": "Science"
      }
    ]
  }
]
```
---
---

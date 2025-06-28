

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

#### Request Structure
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

#### Example Request
**Request Body**
```json
{
  "collectionID": 101,
  "userID": 123,
  "reviewText": "This was a great collection, very informative!",
  "reviewValue": 5,
  "reviewDate": "2024-05-21T14:00:00Z"
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

#### Request Structure
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

#### Example Request
**Request URL**
`/api/v1/collections/reviews?CollectionID=101`

**Request Body**
```json
[
  {
    "op": "replace",
    "path": "/reviewValue",
    "value": 4
  },
  {
    "op": "replace",
    "path": "/reviewText",
    "value": "This was a good collection, but a bit too short."
  }
]
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | The updated `MainCollectionsReviewDTO` object. |
| `403 Forbidden` | Not Owner | The user is trying to update a review that is not theirs. |
| `404 Not Found` | Not Found | The user has not submitted a review for this collection. |

#### Response Structure
**`200 OK` Response Body** (`MainCollectionsReviewDTO`)
```json
{
  "collectionID": 0,
  "userID": 0,
  "reviewText": "string",
  "reviewValue": 0,
  "reviewDate": "2024-05-21T12:00:00Z"
}
```

#### Example Response
**`200 OK` Response Body**
```json
{
  "collectionID": 101,
  "userID": 123,
  "reviewText": "This was a good collection, but a bit too short.",
  "reviewValue": 4,
  "reviewDate": "2024-05-21T14:05:00Z"
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

#### Request Structure
**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CollectionID` | integer | Yes | The ID of the collection whose review is being deleted. |

#### Example Request
**Request URL**
`/api/v1/collections/reviews?CollectionID=101`

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

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CollectionID` | integer | Yes | The ID of the collection. |

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `Page` | integer | No | The page number for pagination (e.g., `1`, `2`, `3`...). |

#### Example Request
**Request URL**
`/api/v1/collections/reviews/101?Page=1`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `MainCollectionsReviewDTO` objects. |
| `404 Not Found` | Not Found | The specified collection does not exist. |

#### Response Structure
**`200 OK` Response Body** (Array of `MainCollectionsReviewDTO`)
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

#### Example Response
**`200 OK` Response Body**
```json
[
  {
    "collectionID": 101,
    "userID": 123,
    "reviewText": "This was a good collection, but a bit too short.",
    "reviewValue": 4,
    "reviewDate": "2024-05-21T14:05:00Z"
  },
  {
    "collectionID": 101,
    "userID": 125,
    "reviewText": "Excellent content!",
    "reviewValue": 5,
    "reviewDate": "2024-05-21T15:00:00Z"
  }
]
```
---
---

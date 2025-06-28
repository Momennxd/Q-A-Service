
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

#### Request Structure
**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CollectionId` | integer | Yes | The ID of the collection to vote on. |
| `IsLike` | boolean | Yes | `true` for a like, `false` for a dislike. |

#### Example Request
**Request URL**
`/api/v1/collections/likes/Like?CollectionId=101&IsLike=true`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | Empty response body, confirming the action was recorded. |
| `401 Unauthorized`| No Token | The request lacks a valid `Authorization` header. |
| `404 Not Found` | Not Found | The specified collection does not exist. |

---
---

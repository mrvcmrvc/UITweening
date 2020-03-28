# MMTweening
Tween Systems for Unity 5 UI

This tween system is inspired from DoTween.

FEATURE:
- All tweens are like unity components, just add the one or more to a game object and adjust the values from the inspector screen.
- Tweens can auto play when their GO became active on the scene, or they can be activated through script.
- All the values that can be changed from inspector can also be changed from script.
- There is also a virtual value tweener that allows you to tween variables like int, float, double, vector3, etc.

CURRENT TWEENS:
- Alpha (Can change the alpha of Image, SpriteRenderer, CanvasGroup),
- Color (Can change the color of Image, SpriteRenderer),
- Position (Can update position through RectTransform, Transform, Rigidbody, Rigidbody2D),
- Rotation,
- Scale,
- Size

There are 11 ease type, each with in, out and InOut variants. Also there is Animation Curve which you can easily define your ease.

NOTES:
- Rotation tween is currently using Euler rotation, allowing for gimbal lock problem.

COMING FEATURES:
 - Quaternion Tween (to eliminate gimbal lock problem)
 - Material Tween (allows to tween material property)
 - Additional settings for rotations

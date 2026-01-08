# Unity – Ball Shooter & Shoot the Cube

This repository contains two Unity laboratory exercises focused on **physics-based interactions**, **raycasting**, and **spatial reasoning** using the built-in Unity physics system.

The project was developed as part of **Module 4 – Laboratory 4**, with increasing levels of difficulty and a strong focus on correct technical implementation rather than visuals.

---

## Exercises Overview

### Exercise 1 – Ball Shooter
A physics-based shooting system where:
- Blue cubes are instantiated dynamically and use **Rigidbody**
- The player shoots spheres using the left mouse button
- A **Physics.SphereCast** is used to verify that the shooting line is free
- If the SphereCast hits a gray wall, the shot is blocked
- Otherwise, a sphere is instantiated and launched forward
- The projectile self-destroys after a configurable amount of time
- The ball can knock down the blue cubes using real physics

**Key concepts:**
- SphereCast with radius
- Rigidbody force / velocity
- Collision detection
- Runtime instantiation

---

### Exercise 2 – Shoot the Cube

#### Part 1 – Easy
- Clicking on a cube applies a force at the exact hit point
- Uses `Rigidbody.AddForceAtPosition`
- Demonstrates torque and rotation based on impact point

#### Part 2 – Medium
- Bullet hole decals are spawned at the click position
- Bullet holes are:
  - Positioned at the collision point
  - Oriented correctly to the surface normal
  - Offset slightly to avoid z-fighting
- Bullet holes are attached to the cube and move with it

#### Part 3 – Hard
- When shooting near cube edges, bullet holes are clamped **inside the face**
- Prevents decals from partially exiting the cube surface
- Uses:
  - Local space conversion
  - Face detection via surface normal
  - Size-aware clamping based on decal scale

---

## What is Z-Fighting?

**Z-fighting** is a rendering artifact that occurs when two surfaces occupy the same (or nearly the same) depth position.

When this happens, the GPU cannot consistently determine which surface should be drawn in front, resulting in:
- Flickering
- Shimmering
- Random visual artifacts

### 🔧 How it was solved in this project
Bullet holes are spawned using a small offset along the surface normal:

```csharp
spawnPosition = hitPoint + hitNormal * surfaceOffset;
````

This ensures the decal is rendered slightly above the surface, completely avoiding z-fighting.

---

## Technical Highlights

* Physics.Raycast & Physics.SphereCast
* Rigidbody.AddForceAtPosition
* Correct use of surface normals
* World ↔ Local space conversions
* Runtime prefab instantiation
* Decal orientation from camera direction
* Edge clamping on 3D surfaces
* No unnecessary colliders on visual-only objects

---

## Purpose of the Project

This repository focuses on **correct technical reasoning**, not visual polish.
It demonstrates:

* Understanding of Unity physics
* Proper spatial math
* Handling of real-world engine limitations
* Clean, production-minded solutions to common gameplay problems

---

## Unity Version

Developed using Unity (Built-in Render Pipeline).

---

```


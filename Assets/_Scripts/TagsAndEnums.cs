using UnityEngine;
using System.Collections;

public class TagsAndEnums : MonoBehaviour {
    public const string player = "Player";
    public const string ignore = "Ignore";
    public const string enemy = "Enemy";
    public const string terrain = "Terrain";
    public const string projectile = "Projectile";
	public const string shootingBox = "ShootingBox";
	public const string levelSelector = "LevelSelector";
    public const string obstacle = "Obstacle";

    public static float GetSqrDistance(Vector3 a, Vector3 b)
    {
        return (a-b).sqrMagnitude;
    }

    public enum ProjectileType
    {
        missile,
		laser,
		homerMissile,
        speedMissile
    };

	public enum AimingDirection
	{
		forward,
		back,
		left,
		right,
		up,
		down
	};
}

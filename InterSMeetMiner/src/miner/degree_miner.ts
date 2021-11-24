import srcjson from "../assets/degrees.json";
import { Degree } from "../models/degree";
import { Family } from "../models/Family";
import { Level } from "../models/Level";

const degreesjson = srcjson as any;

export class DegreeMiner {
  /**
   * This method perform one task:
   *    Map each degree (json) into a Degree (relational model). To accomplish this task it will:
   *    1. Iterate each degree-info (contains degree-name) to extract degree-name
   *    2. Deduce and associate family & level IDs
   *
   * Family & Level IDs are processed incrementally before
   *
   * Objective: Produce unique data with "foreing keys"
   */
  public getDegrees(families?: Family[], levels?: Level[]): Degree[] {
    const degrees: Degree[] = [];
    const _levels: Level[] = levels ?? this.getLevels();
    const _families: Family[] = families ?? this.getFamilies();

    // Iterate each level
    for (const levelname in degreesjson) {
      const level = degreesjson[levelname];

      // Iterate each degree-info
      for (const degree_info_index in level) {
        const degree_info = level[degree_info_index];
        const degrees_in_info = degree_info.degrees;
        const family_name = degree_info.family;

        // Iterate each degree in degree_info.degrees
        for (const degree_index in degrees_in_info) {
          const degree_name = degrees_in_info[degree_index];

          // And save degree with associated level & family
          degrees.push({
            degreeId: (degrees.at(-1)?.degreeId ?? 0) + 1,
            name: degree_name,
            familyId: _families.find((f) => f.name === family_name)
              ?.familyId as number,
            levelId: _levels.find((l) => l.name === levelname)
              ?.levelId as number,
          });
        }
      }
    }

    return degrees;
  }

  public getFamilies(): Family[] {
    const families: Family[] = [];
    const unique_families_names = new Set<string>();

    // Iterate each level
    for (const level_name in degreesjson) {
      const level = degreesjson[level_name];

      // Iterate each degree-info
      for (const degree_info_index in level) {
        const degree_info = level[degree_info_index];
        const family_name = degree_info.family;
        // And save family of degree-info - Set<string> prevents duplicated
        unique_families_names.add(family_name);
      }
    }

    const iterable = Array.from(unique_families_names);
    for (const familyname of iterable) {
      families.push({
        familyId: (families.at(-1)?.familyId ?? 0) + 1,
        name: familyname,
      });
    }

    return families;
  }

  public getLevels(): Level[] {
    const levels: Level[] = [];

    for (const levelname in degreesjson) {
      levels.push({
        levelId: (levels.at(-1)?.levelId ?? 0) + 1,
        name: levelname,
      });
    }

    return levels;
  }
}

import { Configuration } from "./db";
import mysql from "mysql2/promise";
import { DegreeMiner } from "./miner/degree_miner";
import { Family } from "./models/Family";
import { Level } from "./models/Level";

/**
 * @param clean true to clean existing records
 */
async function main(clean?: boolean) {
  // create the connection
  const cnn = await mysql.createConnection(Configuration);
  const s = new DegreeMiner();

  // To insert
  const families = s.getFamilies();
  const levels = s.getLevels();
  const degrees = s.getDegrees(families, levels);

  for (const degree of degrees) {
    const existF = families.find((f) => f.familyId === degree.familyId);
    const existL = levels.find((l) => l.levelId === degree.levelId);

    if (!existF) console.log("Family reference fails");
    if (!existL) console.log("Level reference fails");
  }

  if (clean) {
    cnn.execute("DELETE FROM degree");
    cnn.execute("DELETE FROM family");
    cnn.execute("DELETE FROM level");
  }

  for (const family of families) {
    try {
      await cnn.execute("INSERT INTO family VALUES (?, ?)", [
        family.familyId,
        family.name,
      ]);
    } catch (err) {
      const error = err as any;
      if (error.errno === 1062) console.log("family already exists");
      else console.log("unexpected error while insert family");
    }
  }

  for (const level of levels) {
    try {
      await cnn.execute("INSERT INTO level VALUES (?, ?)", [
        level.levelId,
        level.name,
      ]);
    } catch (err) {
      const error = err as any;
      if (error.errno === 1062) console.log("level already exists");
      else console.log("unexpected error while insert level");
    }
  }

  for (const dg of degrees) {
    try {
      await cnn.execute(
        "INSERT INTO degree (degree_id, name, family_id, level_id) VALUES (?, ?, ?, ?)",
        [dg.degreeId, dg.name, dg.familyId, dg.levelId]
      );
    } catch (err) {
      const error = err as any;
      if (error.errno == 1062) console.log("degree already exists");
      else console.log("unexpected error while insert degree");
    }
  }

  cnn.destroy();
}

main(true);

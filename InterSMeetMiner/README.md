# Data miner for InterSMeet

## Purpose

Process datasets (currently only JSON datasets).
As it's *not planned* to process new datasets *in the future*, the design is oriented to process only three relational models at the moment, which are `Degree`, `Level` (degree-level) and `Family` (degree-family).

## How it works

This miner will read and transform in a relational style data contained in `degrees.json`, which should follow `degrees_demo.json` format.
Then it will place this information into three existing tables called `family`, `level` and `degree`.

### Tables

You can get the tables in `mining-dump.sql` file.

### Next steps

Now that we have data stored in a new fresh database, we can use any database management tool to "dump" this data into a `.sql` script to enable entity framework integrate necessary data into InterSMeet DB :rocket:
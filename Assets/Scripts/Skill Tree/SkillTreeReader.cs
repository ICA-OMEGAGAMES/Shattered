using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SkillTreeReader : MonoBehaviour
{

    private static SkillTreeReader instance;

    public static SkillTreeReader Instance
    {
        get
        {
            return instance;
        }
        set {}
    }

    // Array with all the skills in our skilltree
    private Skill[] skillTree;

    // Dictionary with the skills in our skilltree
    private Dictionary<int, Skill> skills;

    // Variable for caching the currently being inspected skill
    private Skill skillInspected;

    public int availablePoints = 100;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SetUpSkillTree();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization of the skill tree
    void SetUpSkillTree()
    {
        skills = new Dictionary<int, Skill>();

        if (PlayerPrefs.GetString("SkillTree", "").CompareTo("") == 0)
        {
            LoadSkillTree();
        }
        else
        {
            LoadSkillTree();
            // LoadPlayerSkillTree();
            // TODO: right now, we can't save the activated tree, later if the we will save it to the player, the code is right implemented
        }
    }

    public void LoadSkillTree()
    {
        string dataAsJson;
		if (File.Exists(Constants.SKILLTREE_JSON))
        {
            // Read the json from the file into a string
			dataAsJson = File.ReadAllText(Constants.SKILLTREE_JSON);

            // Pass the json to JsonUtility, and tell it to create a SkillTree object from it
            SkillTree loadedData = JsonUtility.FromJson<SkillTree>(dataAsJson);

            // Store the SkillTree as an array of Skill
            skillTree = new Skill[loadedData.skilltree.Length];
            skillTree = loadedData.skilltree;

            // Populate a dictionary with the skill id and the skill data itself
            for (int i = 0; i < skillTree.Length; ++i)
            {
                skills.Add(skillTree[i].idSkill, skillTree[i]);
            }
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }

    public void LoadPlayerSkillTree()
    {
        string dataAsJson = PlayerPrefs.GetString("SkillTree");

        // Pass the json to JsonUtility, and tell it to create a SkillTree object from it
        SkillTree loadedData = JsonUtility.FromJson<SkillTree>(dataAsJson);

        // Store the SkillTree as an array of Skill
        skillTree = new Skill[loadedData.skilltree.Length];
        skillTree = loadedData.skilltree;

        // Populate a dictionary with the skill id and the skill data itself
        for (int i = 0; i < skillTree.Length; ++i)
        {
            skills.Add(skillTree[i].idSkill, skillTree[i]);
        }
    }

    public void SaveSkillTree()
    {
        // We fill with as many skills as nodes we have
        SkillTree tempSkillTree = new SkillTree();
        tempSkillTree.skilltree = new Skill[skillTree.Length];
        for (int i = 0; i < skillTree.Length; ++i)
        {
            skills.TryGetValue(skillTree[i].idSkill, out skillInspected);
            if (skillInspected != null)
            {
                tempSkillTree.skilltree[i] = skillInspected;
            }
        }

        string json = JsonUtility.ToJson(tempSkillTree);

        PlayerPrefs.SetString("SkillTree", json);
    }

    public bool IsSkillUnlocked(int idskill)
    {
        if (skills.TryGetValue(idskill, out skillInspected))
        {
            return skillInspected.unlocked;
        }
        else
        {
            return false;
        }
    }

    public bool CanSkillBeUnlocked(int idskill)
    {
        bool canUnlock = true;
        if (skills.TryGetValue(idskill, out skillInspected)) // The skill exists
        {
            if (skillInspected.cost <= availablePoints) // Enough points available
            {
                int[] dependencies = skillInspected.skillDependencies;
                for (int i = 0; i < dependencies.Length; ++i)
                {
                    if (skills.TryGetValue(dependencies[i], out skillInspected))
                    {
                        if (!skillInspected.unlocked)
                        {
                            canUnlock = false;
                            break;
                        }
                    }
                    else // If one of the dependencies doesn't exist, the skill can't be unlocked.
                    {
                        return false;
                    }
                }
            }
            else // If the player doesn't have enough skill points, can't unlock the new skill
            {
                return false;
            }

        }
        else // If the skill id doesn't exist, the skill can't be unlocked
        {
            return false;
        }
        return canUnlock;
    }

    public bool UnlockSkill(int idSkill)
    {
        if (skills.TryGetValue(idSkill, out skillInspected))
        {
            if (skillInspected.cost <= availablePoints)
            {
                availablePoints -= skillInspected.cost;
                skillInspected.unlocked = true;

                // We replace the entry on the dictionary with the new one (already unlocked)
                skills.Remove(idSkill);
                skills.Add(idSkill, skillInspected);

                return true;
            }
            else
            {
                return false;   // The skill can't be unlocked. Not enough points
            }
        }
        else
        {
            return false;   // The skill doesn't exist
        }
    }

    public string getDescription(int idSkill)
    {
        return skills[idSkill].description;
    }

    public string getSkillCost(int idSkill)
    {
        return skills[idSkill].cost.ToString();
    }
}
using System.Data.Common;
using Arrowgene.Ddon.Shared.Entity.Structure;
using Arrowgene.Ddon.Shared.Model;

namespace Arrowgene.Ddon.Database.Sql.Core
{
    public abstract partial class DdonSqlDb<TCon, TCom> : SqlDb<TCon, TCom>
        where TCon : DbConnection
        where TCom : DbCommand
    {
        private static readonly string[] CDataEquipJobItemFields = new string[]
        {
            "character_common_id", "job", "job_item_id", "equip_slot_no"
        };

        private readonly string SqlInsertEquipJobItem = $"INSERT INTO `ddon_equip_job_item` ({BuildQueryField(CDataEquipJobItemFields)}) VALUES ({BuildQueryInsert(CDataEquipJobItemFields)});";
        private readonly string SqlReplaceEquipJobItem = $"INSERT OR REPLACE INTO `ddon_equip_job_item` ({BuildQueryField(CDataEquipJobItemFields)}) VALUES ({BuildQueryInsert(CDataEquipJobItemFields)});";
        private static readonly string SqlUpdateEquipJobItem = $"UPDATE `ddon_equip_job_item` SET {BuildQueryUpdate(CDataEquipJobItemFields)} WHERE `character_common_id` = @character_common_id AND `job` = @job AND `equip_slot_no`=@equip_slot_no;";
        private static readonly string SqlSelectEquipJobItem = $"SELECT {BuildQueryField(CDataEquipJobItemFields)} FROM `ddon_equip_job_item` WHERE `character_common_id` = @character_common_id AND `job` = @job;";
        private static readonly string SqlSelectEquipJobItemByCharacter = $"SELECT {BuildQueryField(CDataEquipJobItemFields)} FROM `ddon_equip_job_item` WHERE `character_common_id` = @character_common_id;";
        private const string SqlDeleteEquipJobItem = "DELETE FROM `ddon_equip_job_item` WHERE `character_common_id`=@character_common_id AND `job`=@job AND `equip_slot_no`=@equip_slot_no;";

        private CDataEquipJobItem ReadEquipJobItem(DbDataReader reader)
        {
            CDataEquipJobItem equipJobItem = new CDataEquipJobItem();
            equipJobItem.JobItemId = GetUInt32(reader, "job_item_id");
            equipJobItem.EquipSlotNo = GetByte(reader, "equip_slot_no");
            return equipJobItem;
        }

        private void AddParameter(TCom command, uint commonId, JobId job, CDataEquipJobItem equipJobItem)
        {
            AddParameter(command, "character_common_id", commonId);
            AddParameter(command, "job", (byte) job);
            AddParameter(command, "job_item_id", equipJobItem.JobItemId);
            AddParameter(command, "equip_slot_no", equipJobItem.EquipSlotNo);
        }
    }
}
namespace JudgeBot.Core.Models;

public class Act
{
    /// <summary>
    ///  Ключ дела
    /// </summary>
    public Guid Uid { get; init; }

    /// <summary>
    ///  Имя дела
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///  Описание дела
    /// </summary>
    public string Description { get; set; } = null!;
    /// <summary>
    ///  ID чата
    /// </summary>
    public long ChatId { get; set; }

    /// <summary>
    ///  ID обвиняемого
    /// </summary>
    public long DefendantId { get; set; }

    /// <summary>
    ///  ID жертвы
    /// </summary>
    public long? VictimId { get; set; }

    /// <summary>
    ///  ID судьи
    /// </summary>
    public long? MagistrateId { get; set; }

    /// <summary>
    ///  ID обвинителя
    /// </summary>
    public long AccuserId { get; set; }

    /// <summary>
    ///  Uid решения по делу
    /// </summary>
    public Guid? ResolutionUid { get; set; }

    /// <summary>
    ///  Uid статуса дела
    /// </summary>
    public Guid StatusUid { get; set; }

    /// <summary>
    ///  Дата создания дела
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    ///  Дата изменения дела
    /// </summary>
    public DateTime EditedAt { get; set; }

    /// <summary>
    ///  ID пользователя, который изменил дело
    /// </summary>
    public long EditUserId { get; set; }

    /// <summary>
    ///  ID пользователя, который создал дело
    /// </summary>
    public long CreateUserId { get; init; }
    
    /// <summary>
    ///  Обвиняемый
    /// </summary>
    public virtual User Defendant { get; set; }
    /// <summary>
    ///  Жертва
    /// </summary>
    public virtual User? Victim { get; set; }
    /// <summary>
    ///  Судья
    /// </summary>
    public virtual User Magistrate { get; set; }
    /// <summary>
    ///  Обвинитель
    /// </summary>
    public virtual User Accuser { get; set; }
    /// <summary>
    ///  Решение по делу
    /// </summary>
    public virtual ActResolution? Resolution { get; set; }
    /// <summary>
    ///  Статус дела
    /// </summary>
    public virtual Status Status { get; set; }
    /// <summary>
    ///  Создавший пользователь
    /// </summary>
    public virtual User CreateUser { get; init; }
    /// <summary>
    ///  Изменивший пользователь
    /// </summary>
    public virtual User EditUser { get; init; }
    /// <summary>
    ///  Те, кто принимал участие в суде
    /// </summary>
    public virtual IList<User>? Jury { get; set; } // Присяжные
}
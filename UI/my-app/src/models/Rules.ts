export interface BaseRule {
    Id: string;
    RuleName: string;
}

export interface TeacherBusyDayRule extends BaseRule {
    ActorId: string,
    Target: number
}
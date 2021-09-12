export class Attendance{
    public attendanceDate : Date;
    public totalTime : number=1.5;
    public dayName : string="";
    constructor(attendanceDate:Date, totalTime: number , dayname : string){
        this.attendanceDate = attendanceDate;
        this.totalTime = totalTime;
        this.dayName = dayname;
    }
}
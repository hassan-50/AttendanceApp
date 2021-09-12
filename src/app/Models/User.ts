export class User{
    public fullName : string="";
    public email : string="";
    public userName : string="";
    public role : string="";
    public checked : number=0;
    public enabled : boolean=true;
    constructor(fullname:string, email: string , username : string , role:string , checked:number , enabled:boolean){
        this.fullName = fullname;
        this.email = email;
        this.userName = username;
        this.role = role;
        this.checked = checked;
        this.enabled = enabled;
    }
}
export class Role {
  public id: string = '';
  public name: string = '';
}

export class User {
  public id: string = '';
  public userName: string = '';
  public password: string = '';
  public roles: Role[] = [];
}

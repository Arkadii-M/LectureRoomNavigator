export class Role {
  public id: string = '';
  public name: string = '';
}

export class User {
  public id: string = '';
  public username: string = '';
  public password: string = '';
  public roles: Role[] = [];
}

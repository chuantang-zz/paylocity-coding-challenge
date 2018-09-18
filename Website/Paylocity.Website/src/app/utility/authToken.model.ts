export class AuthToken
{
	public id: string;
	public auth_token: string;
	public expires_in: string;

	constructor()
	{
		this.id = '';
		this.auth_token = '';
		this.expires_in = '';
	}
}

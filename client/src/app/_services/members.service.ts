import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

// const httpOptions = {
//   headers: new HttpHeaders({
//     Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token
//   })
// }

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;

  members: Member[] = [];

  constructor(private http: HttpClient) { }

  getMembers(){
    // return this.http.get<Member[]>(this.baseUrl + 'users', httpOptions);
    // return this.http.get<Member[]>(this.baseUrl + 'users');

    if(this.members.length > 0) return of(this.members);
    return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
      map(members => {
        this.members = members;
        return members;
      })
    )
  }

  getMember(username: string){
    // return this.http.get<Member>(this.baseUrl + 'users/', httpOptions);
    // return this.http.get<Member>(this.baseUrl + 'users/' + username);

    const member = this.members.find(x => x.username === username);
    if(member !== undefined) return of(member);
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  updateMember(member: Member){
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    )
  }

  
  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photoId);
  }
  

  // addLike(username: string) {
  //   return this.http.post(this.baseUrl + 'likes/' + username, {})
  // }

  // getLikes(predicate: string, pageNumber, pageSize) {
  //   let params = getPaginationHeaders(pageNumber, pageSize);
  //   params = params.append('predicate', predicate);
  //   return getPaginatedResult<Partial<Member[]>>(this.baseUrl + 'likes', params, this.http);
  // }
}

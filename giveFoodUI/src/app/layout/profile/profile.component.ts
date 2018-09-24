import { ProfileModel } from './../../shared/services/user.model';
import { ProfileService, AuthService } from './../../shared/services';
import { Component, OnInit } from '@angular/core';
import {  FileUploader } from 'ng2-file-upload/ng2-file-upload';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';

const URL = '';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  userData: ProfileModel;
  documentName: string;
  profileFormGroup: FormGroup;
  public uploader: FileUploader = new FileUploader({url: URL, itemAlias: 'photo'});

  constructor(private profileService: ProfileService, private authService: AuthService, private formBuilder: FormBuilder) {
    this.userData = new ProfileModel('', '', '',  0);
  }

  ngOnInit() {
    this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false; };
    this.uploader.onCompleteItem = (item: any, response: any, status: any, headers: any) => {
         this.profileService.uploadFile(item.file).subscribe(x => x);
         console.log('ImageUpload:uploaded:', item, status, response);
         alert('File uploaded successfully');
     };
      this.profileService.get().subscribe((data: ProfileModel) => {
        this.profileFormGroup.patchValue(data);
         this.documentName = data.documentName;
      });
      this.profileFormGroup = this.formBuilder.group({
            name: [this.userData.name, Validators.required],
            email: [this.userData.email, null],
            description: [this.userData.description, null]
        });
    }
  isAdmin(): boolean {
    return this.authService.isAdmin();
  }
  onSubmit() {
      this.profileService.update(this.profileFormGroup.value)
      .subscribe(data => data);
  }
}

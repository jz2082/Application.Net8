import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { PrimengModule } from '@common/primeng.module';
 
@Component({
  selector: 'pluralsight-profile',
  standalone: true,
  imports: [
    CommonModule,
    PrimengModule
  ],
  providers: [
  ],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit {
  profileImage: string = '';
  images = [
    { previewImageSrc: "https://i.pravatar.cc/300?u=Anne", thumbnailImageSrc: "https://i.pravatar.cc/50?u=Anne", alt: "Anne", title: "Anne" },
    { previewImageSrc: "https://i.pravatar.cc/300?u=Kerri", thumbnailImageSrc: "https://i.pravatar.cc/50?u=Kerri", alt: "Kerri", title: "Kerri" },
    { previewImageSrc: "https://i.pravatar.cc/300?u=Mary", thumbnailImageSrc: "https://i.pravatar.cc/50?u=Mary", alt: "Mary", title: "Mary" },
    { previewImageSrc: "https://i.pravatar.cc/300?u=Nancy", thumbnailImageSrc: "https://i.pravatar.cc/50?u=Nancy", alt: "Nancy", title: "Nancy" },
    { previewImageSrc: "https://i.pravatar.cc/300?u=Peta", thumbnailImageSrc: "https://i.pravatar.cc/50?u=Peta", alt: "Peta", title: "Peta" },
  ];
  selectedProfile: any;

  constructor(private messageService: MessageService) { }

  ngOnInit(): void {
  }

  onImageSelected(event: any) {
    console.log(JSON.stringify(event));
  }

  onDragStart(galleria: { activeIndex: number }) {
    this.selectedProfile = this.images[galleria.activeIndex];
  }

  onPicDrop() {
    this.profileImage = this.selectedProfile.previewImageSrc;
    this.messageService.add({ key: 'componentMsg', severity: 'info', summary: 'New Profile', detail: `Changed pic to ${this.selectedProfile.title}` });
  }
}
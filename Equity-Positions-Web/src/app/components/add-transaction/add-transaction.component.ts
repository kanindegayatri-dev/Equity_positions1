import { Component, OnInit } from '@angular/core';
import { TransactionsService } from 'src/app/services/transactions.service';
import { Transaction } from '../../models/trasaction.model';


@Component({
  selector: 'app-add-transaction',
  templateUrl: './add-transaction.component.html',
  styleUrls: ['./add-transaction.component.css']
})
export class AddTransactionComponent  {

  message : string = '';
  transaction : Transaction = {
    tradeId:0,
    version:1,
    securityCode:'',
    quantity:0,
    action:'INSERT',
    buySell:'Buy'
  };

  constructor(private transactionsService : TransactionsService) { }

  onSubmit(){
    this.transactionsService.addTransaction(this.transaction).subscribe(
      next => {
        this.message = 'Transaction added successfully!';
        
      },
      error => {
        this.message = 'Error adding transaction.';
       
      }
    );
}}

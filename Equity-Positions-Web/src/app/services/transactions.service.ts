import { Injectable } from '@angular/core';
import { Transaction } from '../models/trasaction.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TransactionsService {

  constructor(private http: HttpClient) { }

  private apiUrl = 'https://localhost:44385/api/transactions';

  // POST request to add a new transaction
  addTransaction(transaction: Transaction): Observable<any> {
    return this.http.post(this.apiUrl, transaction);
    };

  getTransactions(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(this.apiUrl);
  }

  getTransactionById(tradeId: number): Observable<Transaction> {
    const url = `${this.apiUrl}/${tradeId}`;
    return this.http.get<Transaction>(url);
  }

  updateTransaction(tradeId: number, transaction: Transaction): Observable<any> {
    const url = `${this.apiUrl}/${tradeId}`;
    return this.http.put(url, transaction);
  }

  deleteTransaction(tradeId: number): Observable<any> {
    const url = `${this.apiUrl}/${tradeId}`;
    return this.http.delete(url);
  }
}

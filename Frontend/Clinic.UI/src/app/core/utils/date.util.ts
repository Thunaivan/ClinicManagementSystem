import { formatDate } from '@angular/common';

export class DateUtil {
  private static readonly API_DATE_FORMAT = 'yyyy-MM-dd';
  private static readonly LOCALE = 'en-GB';

  static toApiDate(date: Date | string | null | undefined): string {
    if (!date) {
      return '';
    }

    return formatDate(date, this.API_DATE_FORMAT, this.LOCALE);
  }
}

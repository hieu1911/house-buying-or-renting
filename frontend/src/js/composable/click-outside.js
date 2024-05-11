import { useEventListener } from './event-listener';
import common from '../common/helper';

/**
 * Composable theo dõi sự kiện click ra ngoài phần tử
 * @param {Element} rootEl - Phần tử gốc
 * @param {Function} callback - Hàm thực thi
 * Created by: DVHIEU (19/8/2023)
 */
export function useOnClickOutside(rootEl, callback) {
    try {
        useEventListener(window, 'mouseup', (e) => {
            const clickedEl = e.target;
            
            // Bỏ qua nếu phần tử gốc chứa phần tử được click
            if (rootEl.contains(clickedEl)) {
                return;
            }
    
            // Thực thi hành động
            callback();
        });
    } catch (err) {
        common.handleError(err);
    }
}

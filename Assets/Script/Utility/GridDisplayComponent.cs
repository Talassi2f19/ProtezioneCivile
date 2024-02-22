using UnityEngine;
using UnityEngine.UI;
//TODO Check Warning
namespace Script.Utility
{
    [AddComponentMenu("Layout/Flow Layout Group", 153)]
    public class FlowLayoutGroup : LayoutGroup
    {
        public enum Corner
        {
            UpperLeft = 0,
            UpperRight = 1,
            LowerLeft = 2,
            LowerRight = 3
        }

        public enum Constraint
        {
            Flexible = 0,
            FixedColumnCount = 1,
            FixedRowCount = 2
        }

        protected Vector2 mCellSize = new Vector2(100, 100);
        public Vector2 CellSize
        {
            get { return mCellSize; }
            set { SetProperty(ref mCellSize, value); }
        }

        [SerializeField] protected Vector2 mSpacing = Vector2.zero;
        public Vector2 Spacing
        {
            get { return mSpacing; }
            set { SetProperty(ref mSpacing, value); }
        }

        protected FlowLayoutGroup()
        { }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
        }

#endif

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            int minColumns = 0;
            int preferredColumns = 0;

            minColumns = 1;
            preferredColumns = Mathf.CeilToInt(Mathf.Sqrt(rectChildren.Count));

            SetLayoutInputForAxis(
                padding.horizontal + (CellSize.x + Spacing.x) * minColumns - Spacing.x,
                padding.horizontal + (CellSize.x + Spacing.x) * preferredColumns - Spacing.x,
                -1, 0);
        }

        public override void CalculateLayoutInputVertical()
        {
            int minRows = 0;

            float width = rectTransform.rect.size.x;
            int cellCountX = Mathf.Max(1, Mathf.FloorToInt((width - padding.horizontal + Spacing.x + 0.001f) / (CellSize.x + Spacing.x)));
            //      minRows = Mathf.CeilToInt(rectChildren.Count / (float)cellCountX);
            minRows = 1;
            float minSpace = padding.vertical + (CellSize.y + Spacing.y) * minRows - Spacing.y;
            SetLayoutInputForAxis(minSpace, minSpace, -1, 1);
        }

        private void Update()
        {
            SetLayoutHorizontal();
            SetLayoutVertical();
        }

        public override void SetLayoutHorizontal()
        {
            SetCellsAlongAxis(0);
        }

        public override void SetLayoutVertical()
        {
            SetCellsAlongAxis(1);
        }

        int cellsPerMainAxis, actualCellCountX, actualCellCountY;
        int positionX;
        int positionY;
        float totalWidth = 0;
        float totalHeight = 0;

        float lastMaxHeight = 0;

        private void SetCellsAlongAxis(int axis)
        {
            // Normally a Layout Controller should only set horizontal values when invoked for the horizontal axis
            // and only vertical values when invoked for the vertical axis.
            // However, in this case we set both the horizontal and vertical position when invoked for the vertical axis.
            // Since we only set the horizontal position and not the size, it shouldn't affect children's layout,
            // and thus shouldn't break the rule that all horizontal layout must be calculated before all vertical layout.

            float width = rectTransform.rect.size.x;
            float height = rectTransform.rect.size.y;

            int cellCountX = 1;
            int cellCountY = 1;

            if (CellSize.x + Spacing.x <= 0)
                cellCountX = int.MaxValue;
            else
                cellCountX = Mathf.Max(1, Mathf.FloorToInt((width - padding.horizontal + Spacing.x + 0.001f) / (CellSize.x + Spacing.x)));

            if (CellSize.y + Spacing.y <= 0)
                cellCountY = int.MaxValue;
            else
                cellCountY = Mathf.Max(1, Mathf.FloorToInt((height - padding.vertical + Spacing.y + 0.001f) / (CellSize.y + Spacing.y)));

            cellsPerMainAxis = cellCountX;
            actualCellCountX = Mathf.Clamp(cellCountX, 1, rectChildren.Count);
            actualCellCountY = Mathf.Clamp(cellCountY, 1, Mathf.CeilToInt(rectChildren.Count / (float)cellsPerMainAxis));

            Vector2 requiredSpace = new Vector2(
                actualCellCountX * CellSize.x + (actualCellCountX - 1) * Spacing.x,
                actualCellCountY * CellSize.y + (actualCellCountY - 1) * Spacing.y
            );
            Vector2 startOffset = new Vector2(
                GetStartOffset(0, requiredSpace.x),
                GetStartOffset(1, requiredSpace.y)
            );

            totalWidth = 0;
            totalHeight = 0;
            Vector2 currentSpacing = Vector2.zero;
            for (int i = 0; i < rectChildren.Count; i++)
            {
                SetChildAlongAxis(rectChildren[i], 0, startOffset.x + totalWidth /*+ currentSpacing[0]*/, rectChildren[i].rect.size.x);
                SetChildAlongAxis(rectChildren[i], 1, startOffset.y + totalHeight  /*+ currentSpacing[1]*/, rectChildren[i].rect.size.y);

                currentSpacing = Spacing;

                totalWidth += rectChildren[i].rect.width + currentSpacing[0];

                if (rectChildren[i].rect.height > lastMaxHeight)
                {
                    lastMaxHeight = rectChildren[i].rect.height;
                }

                if (i < rectChildren.Count - 1)
                {
                    if (totalWidth + rectChildren[i + 1].rect.width + currentSpacing[0] > width)
                    {
                        totalWidth = 0;
                        totalHeight += lastMaxHeight + currentSpacing[1];
                        lastMaxHeight = 0;
                    }
                }
            }
        }
    }
}